using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class UGUIVertexColor : BaseMeshEffect
    {

        [SerializeField]
        private Color colorLT = Color.white;
        [SerializeField]
        private Color colorRT = Color.white;
        [SerializeField]
        private Color colorLB = Color.white;
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private Color colorRB = Color.white;

        private readonly List<UIVertex> _vertexList = new List<UIVertex>();
        private UIVertex _vertex;
        private Image _image;

        public override void ModifyMesh(VertexHelper vertex)
        {
            _vertexList.Clear();
            vertex.GetUIVertexStream(_vertexList);
            SetColor(0, colorLB);
            SetColor(1, colorLT);
            SetColor(2, colorRT);
            SetColor(3, colorRT);
            SetColor(4, colorRB);
            SetColor(5, colorLB);

            vertex.Clear();
            vertex.AddUIVertexTriangleStream(_vertexList);
        }

        private void SetColor(int index, Color color)
        {
            _vertex = _vertexList[index];
            _vertex.color = color;
            _vertexList[index] = _vertex;
        }

        public void SetVerticesDirty()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            _image.SetVerticesDirty();
        }
    }
}