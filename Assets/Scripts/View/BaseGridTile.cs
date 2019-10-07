using BoardGameModels;
using Database;
using Extensions;
using GridCreating;
using Model;
using System.Linq;
using UnityEngine;

namespace View
{
    public abstract class BaseGridTile : BaseViewModel
    {
        protected const float DIST = .5f;
        public Vector2 Center { get; set; }
        public int Sides { get { return Core.Globals.SideCount; } }
        public Field Field { get; set; }

        protected MeshRenderer signRenderer;

        public override string Id
        {
            get
            {
                return Field?.Id;
            }
        }

        protected Vector3[] points;
        protected MeshFilter filter;
        protected MeshCollider meshCollider;
        protected abstract Material GridMaterial
        {
            get;
        }

        private void Awake()
        {
            signRenderer = GetComponentsInChildren<MeshRenderer>().FirstOrDefault(x => x.tag == "GridSign");
            filter = gameObject.GetComponent<MeshFilter>();
            meshCollider = GetComponent<MeshCollider>();
            gameObject.layer = (int)Enums.LayerMasks.GridTile;
        }

        public virtual void ModifyHeight() { }

        public virtual void SetSignMaterial() { }

        public void GenerateMesh()
        {
            GeneratePoints();
            GetComponent<MeshRenderer>().material = GridMaterial;
            /*GetComponent<MeshRenderer>().material = GridMaterial;
            Triangulator tr = new Triangulator(points.Select(x => x.ToVector2()).ToArray());
            int[] triangles = tr.Triangulate();
            Mesh mesh = new Mesh();
            mesh.vertices = points;
            mesh.triangles = triangles.ToArray();
            filter.mesh = mesh;
            meshCollider.sharedMesh = mesh;*/
            transform.position = new Vector3(Center.x, 0, Center.y);
            SetSignMaterial();
            ModifyHeight();
        }

        public override void LoadModel()
        {
            transform.SetParent(GridTileCreator.Instance.TileParent);
        }

        private void GeneratePoints()
        {
            points = new Vector3[Sides];
            float anglePerSide = 360 / Sides;
            for (int i = 0; i < Sides; i++)
            {
                points[i] = GeometryExtensions.Polar2CartesianDeg(Center, anglePerSide * i, DIST).ToVector3();
            }
        }

        public abstract void OnClick();
    }
}