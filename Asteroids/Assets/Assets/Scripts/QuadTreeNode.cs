using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode
{
	// Boundaries of this TreeNode
	Bounds boundary;

	// Objects in this quad tree node
	List<GameObject> objects;

	// Children
	QuadTreeNode northWest;
	QuadTreeNode northEast;
	QuadTreeNode southWest;
	QuadTreeNode southEast;

	// Constructor
	public QuadTreeNode (Bounds boundary) {
		this.boundary = boundary;
		this.objects = new List<GameObject> ();
		Debug.Log (boundary);
		northWest = null;
		northEast = null;
		southWest = null;
		southEast = null;
	}

	/// <summary>
	/// Insert the specified GameObject into the Tree.
	/// </summary>
	/// <param name="obj">Object.</param>
	public bool Insert(GameObject obj) {
		
		Vector2 p = obj.transform.position;

		// Stop immediately if this node cannot encapsulate the current node
		if (!this.Encapsulates(obj)) {
			//Debug.Log ("Not inside");
			return false;
		}

		// If it can, subdivide and then add the Object to whichever node will accept it, if any
		if (northWest == null)
			Subdivide();

		if (northWest.Insert (obj)) {
			return true;
		} else if (northEast.Insert (obj)) {
			return true;
		} else if (southWest.Insert (obj)) {
			return true;
		} else if (southEast.Insert (obj)) {
			return true;
		}

		// If the Object fits in this node, but no children, add it.
		else {			
			objects.Add(obj);
			return true;
		}

	}

	/// <summary>
	/// Remove the specified GameObject from the Tree.
	/// </summary>
	/// <param name="obj">Object.</param>
	public bool Remove(GameObject obj) {
		Vector2 p = obj.transform.position;

		// Stop immediately if this node cannot encapsulate the current node
		if (!this.Encapsulates(obj)) {
			return false;
		}

		// If our node has yet to subdivide, and it does encapsulate the object, this node holds the object
		if (northWest == null) {
			objects.Remove(obj);
			return true;
		}

		// Otherwise, if we have subdivided, see if our children have it
		if (northWest.Remove (obj)) {
			return true;
		} else if (northEast.Remove (obj)) {
			return true;
		} else if (southWest.Remove (obj)) {
			return true;
		} else if (southEast.Remove (obj)) {
			return true;
		}

		// If the Object fits in this node, but no children contain it, remove it, as it must be in this node
		else {			
			objects.Remove(obj);
			return true;
		}
	}

	public void Update(QuadTreeNode root) {
		List<GameObject> toReinsert = new List<GameObject> ();

		// If our node has yet to subdivide, and it does encapsulate the object, this node holds the object
		if (northWest != null) {

			// If we have subdivided, see if our children have any nodes they no longer contain
			northWest.Update(root);
			northEast.Update(root);
			southWest.Update(root);
			southEast.Update(root);
		}

		// Find all objects no longer contained in this node
		foreach (GameObject obj in objects) {
			if (obj != null && !this.Encapsulates(obj)) {
				toReinsert.Add (obj);
			}
		}


		// Remove and reinsert all objects no longer contained in this node
		foreach (GameObject obj in toReinsert) {
			objects.Remove (obj);
			root.Insert (obj);
		}

		for (int i = 0; i < objects.Count; i++) {
			if (objects[i] == null) {
				objects.Remove (objects[i]);
				i--;
			}
		}
	}

	public void DrawTree() {
		Vector3 bottomLeft = boundary.center - boundary.extents;
		Vector3 topRight = boundary.center + boundary.extents;
		Vector3 bottomRight = boundary.center + new Vector3 (boundary.extents.x, -boundary.extents.y);
		Vector3 topLeft = boundary.center + new Vector3 (-boundary.extents.x, boundary.extents.y);

		Debug.DrawLine (bottomLeft, topLeft);
		Debug.DrawLine (topLeft, topRight);
		Debug.DrawLine (topRight, bottomRight);
		Debug.DrawLine (bottomRight, bottomLeft);

		// If our node has yet to subdivide, do not draw smaller boxes, since they don't exist
		if (northWest != null) {

			// If we have subdivided, draw our children
			northWest.DrawTree();
			northEast.DrawTree();
			southWest.DrawTree();
			southEast.DrawTree();
		}
	}

	/// <summary>
	/// Checks if projectile collides with anything in the tree.
	/// If so, returns the object collided with.
	/// Otherwise, returns null.
	/// </summary>
	/// <param name="projectile">Projectile.</param>
	public GameObject Collides(GameObject projectile) {
		Vector2 p = projectile.transform.position;
		GameObject collider = null;

		// Stop immediately if this node cannot encapsulate the current Object
		if (!this.Encapsulates(projectile)) {
			return null;
		}

		// If our node has yet to subdivide, and it does encapsulate the object,
		// this node would hold any objects that the projectile collides with
		if (northWest != null) {

			// Otherwise, if we have subdivided, see if our children have it
			if ((collider = northWest.Collides (projectile)) != null) {
				return collider;
			} else if ((collider = northEast.Collides (projectile)) != null) {
				return collider;
			} else if ((collider = southWest.Collides (projectile)) != null) {
				return collider;
			} else if ((collider = southEast.Collides (projectile)) != null) {
				return collider;
			}
		}

		// If the Object fits in this node, but no children contain collisions with it, 
		// check this node's objects, as any collisions must be contained in this node.
		foreach (GameObject obj in objects) {
			if (obj != null && Collision.AreCollided (projectile, obj)) {
				return obj;
			}
		}

		// If no collision is found, return null
		return null;
	}

	/// <summary>
	/// Create four children that fully divide this quad into four quads of equal area
	/// </summary>
	private void Subdivide() {
		Vector2 center = boundary.center;
		Vector2 size = boundary.extents / 2;
		if(boundary.extents.x > .001f) {

		// top left
		Vector2 northWestCenter = center - size;
		northWest = new QuadTreeNode(new Bounds (northWestCenter, size * 2));

		// top right
		Vector2 northEastCenter = center + new Vector2(size.x, -size.y);
		northEast = new QuadTreeNode(new Bounds (northEastCenter, size * 2));

		// bottom left
		Vector2 southWestCenter = center + new Vector2(-size.x, size.y);
		southWest = new QuadTreeNode(new Bounds (southWestCenter, size * 2));

		// bottom right
		Vector2 southEastCenter = center + size;
		southEast = new QuadTreeNode(new Bounds (southEastCenter, size * 2));

		//Debug.Log ("SUBDIVIDE: " + boundary + " " + northWestCenter + " " + northEastCenter + " " + southWestCenter + " " + southEastCenter);
		}
	}

	/// <summary>
	/// Tells whether or not this node encapsulates the specified GameObject's boundingCircle.
	/// </summary>
	/// <param name="boundingCircle">GameObject to examine.</param>
	public bool Encapsulates(GameObject boundingCircle) {
		Vector2 p = boundingCircle.GetComponent<SpriteRenderer> ().bounds.center;
		float radius = boundingCircle.GetComponent<SpriteRenderer> ().bounds.extents.y;

		// Ignore objects that do not belong in this quad tree
		bool encapsulates = 
			boundary.Contains (p + new Vector2 (radius, 0)) &&
			boundary.Contains (p + new Vector2 (-radius, 0)) &&
			boundary.Contains (p + new Vector2 (0, radius)) &&
			boundary.Contains (p + new Vector2 (0, -radius));


		//Debug.Log (boundary + " CONTAINS: " + p + " RADIUS: " + radius + " IS: " + encapsulates);
		return encapsulates;
	}

}
