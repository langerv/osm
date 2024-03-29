using System;
using System.Collections.Generic;

namespace csg
{
    /// <summary>
    /// Represents a convex polygon. The vertices used to initialize a polygon must
    /// be coplanar and form a convex loop.
    /// 
    /// Each convex polygon has a `shared` property, which is shared between all
    /// polygons that are clones of each other or were split from the same polygon.
    /// This can be used to define per-polygon properties (such as surface color).
    /// </summary>
    public class CsgPolygon
    {
        public IVertex[] vertices;
        public Object shared; // TODO: maybe this should be a Dictionary ??
        public Plane plane;

        public CsgPolygon(IVertex[] vertices)
        {
            this.vertices = vertices;
            this.plane = Plane.fromPoints(vertices[0].pos, vertices[1].pos, vertices[2].pos);
        }

        public CsgPolygon(IVertex[] vertices, Object shared)
        {
            this.vertices = vertices;
            this.shared = shared;
            this.plane = Plane.fromPoints(vertices[0].pos, vertices[1].pos, vertices[2].pos);
        }

        public CsgPolygon(List<IVertex> vertices)
        {
            this.vertices = vertices.ToArray();
            this.plane = Plane.fromPoints(vertices[0].pos, vertices[1].pos, vertices[2].pos);
        }


        public CsgPolygon(List<IVertex> vertices, Object shared)
        {
            this.vertices = vertices.ToArray();
            this.shared = shared;
            this.plane = Plane.fromPoints(vertices[0].pos, vertices[1].pos, vertices[2].pos);
        }

        public CsgPolygon clone()
        {
            List<IVertex> vs = new List<IVertex>();
            foreach (IVertex v in this.vertices)
            {
                vs.Add(v.clone());
            }
            return new CsgPolygon(vs.ToArray(), this.shared);
        }

        public void flip()
        {
            Array.Reverse(this.vertices, 0, this.vertices.Length);
            foreach (IVertex v in this.vertices)
            {
                v.flip();
            }
            this.plane.flip();
        }
    }
}