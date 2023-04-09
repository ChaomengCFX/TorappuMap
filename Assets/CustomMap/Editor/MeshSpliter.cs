using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Torappu.Battle;

namespace CustomMap {
    public class MeshSpliter : OdinEditorWindow
    {
        [MenuItem("CustomMap/Mesh Split")]
        private static void OpenWindow()
        {
            MeshSpliter spliter = GetWindow<MeshSpliter>();
            m_onSelectionChanged = spliter.OnSelectionChanged;
            Selection.selectionChanged += m_onSelectionChanged;
            spliter.Show();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= m_onSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            GameObject selectedObj = Selection.activeGameObject;
            if (!selectedObj || !selectedObj.activeInHierarchy) return;
            try
            {
                mesh = selectedObj?.GetComponent<MeshFilter>()?.sharedMesh;
                meshRenderer = selectedObj?.GetComponent<MeshRenderer>();
                tile = selectedObj?.GetComponent<TileGraphic>()?._tile;
                tileName = tile?._tileKey;
            } 
            catch (MissingComponentException) { }
        }

        private static Action m_onSelectionChanged;

        [SerializeField]
        private Mesh mesh;
        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private Tile tile;
        [SerializeField, ShowIf("@tile!=null")]
        private string tileName;
        [SerializeField, ShowIf("@tile!=null")]
        private string tileDescription;
        [SerializeField, ShowIf("@tile==null")]
        private string meshName;

        [Button("切割选中物体并生成预制体")]
        private void Split(string path = "CustomMap/Mesh", string prefabPath = "CustomMap/TilePrefabs", bool generatePrefab = true)
        {
            Mesh meshSplited = SplitFrom(mesh, meshRenderer, meshRenderer.transform.position);
            if (meshSplited == null) return;
            DirectoryInfo meshDir = new DirectoryInfo(Path.Combine(Application.dataPath, path));
            if (!meshDir.Exists) meshDir.Create();
            AssetDatabase.CreateAsset(meshSplited, $"Assets/{path}/{meshRenderer.name}.asset");
            if (!generatePrefab) return;
            if (tile != null)
            {
                GameObject prefab = new GameObject(tileName);
                GameObject tilePrefab = Instantiate(tile.gameObject, new Vector3(0f, 0f, -tile._height), Quaternion.Euler(Vector3.zero), prefab.transform);
                tilePrefab.name = tile._tileKey;
                GameObject meshPrefab = Instantiate(meshRenderer.gameObject, new Vector3(0f, 0f, -tile._height), Quaternion.Euler(Vector3.zero), prefab.transform);
                meshPrefab.name = meshSplited.name;
                meshPrefab.GetComponent<MeshFilter>().mesh = meshSplited;
                CustomTile cusTile = prefab.AddComponent<CustomTile>();
                cusTile.tile = tilePrefab.GetComponent<Tile>();
                cusTile.tile._graphic = null;
                cusTile.tile._allGraphicList.Clear();
                cusTile.mesh = meshPrefab.GetComponent<TileGraphic>();
                cusTile.mesh._tile = null;
                cusTile.mesh._gridPos = new Torappu.GridPosition { col = 0, row = 0 };
                cusTile.description = tileDescription;
                DirectoryInfo prefabDir = new DirectoryInfo(Path.Combine(Application.dataPath, prefabPath));
                if (!prefabDir.Exists) prefabDir.Create();
                Selection.activeObject = PrefabUtility.CreatePrefab($"Assets/{prefabPath}/{tileName}.prefab", prefab);
                DestroyImmediate(prefab);
            }
            else
            {
                GameObject meshPrefab = Instantiate(meshRenderer.gameObject, Vector3.zero, Quaternion.Euler(Vector3.zero));
                meshPrefab.name = meshName;
                meshPrefab.GetComponent<MeshFilter>().mesh = meshSplited;
                Selection.activeObject = PrefabUtility.CreatePrefab($"Assets/{prefabPath}/{meshName}.prefab", meshPrefab);
                DestroyImmediate(meshPrefab);
            }
        }

        public static Mesh SplitFrom(Mesh mesh, MeshRenderer render, Vector3 basePos)
        {
            if (!render.isPartOfStaticBatch)
            {
                Debug.LogWarning($"The {render} isn't a part of static batch");
                return null;
            }
            return SplitFrom(mesh, GetSubMeshIndex(render), basePos);
        }

        public static Mesh SplitFrom(Mesh mesh, int subMeshIndex, Vector3 basePos)
        {
            Debug.Log("Spliting subMeshIndex: " + subMeshIndex);
            Mesh newmesh = new Mesh();

            List<Vector3> o_vers = new List<Vector3>(); // 旧顶点数据
            List<Vector3> o_nors = new List<Vector3>(); // 旧法向数据
            List<Vector2> o_uvs = new List<Vector2>(); // 旧UV数据

            // 从模板mesh获得数据
            mesh.GetVertices(o_vers);
            mesh.GetNormals(o_nors);
            mesh.GetUVs(0, o_uvs); // UV通道1 贴图

            List<int> n2o = new List<int>(); // 新索引到旧索引的映射列表
            int[] o_tris = mesh.GetTriangles(subMeshIndex); // 旧的三角形顶点索引
            int[] n_tris = new int[o_tris.Length]; // 新的三角形顶点索引

            for (int i = 0; i < o_tris.Length; i++) // 遍历三角形顶点
            {
                int o_index = o_tris[i]; // 该顶点的旧索引
                int n_index; // 该顶点的新索引

                if (!n2o.Contains(o_index)) // 如果映射列表不存在旧索引则添加
                {
                    n_index = n2o.Count;
                    n2o.Add(o_index);
                }
                else
                {
                    n_index = n2o.IndexOf(o_index);
                }

                n_tris[i] = n_index;
            }

            List<Vector3> n_vers = new List<Vector3>(n2o.Count);
            List<Vector3> n_nors = new List<Vector3>(n2o.Count);
            List<Vector2> n_uvs = new List<Vector2>(n2o.Count);

            for (int i = 0; i < n2o.Count; i++) // 遍历添加新mesh顶点数据
            {
                int o_index = n2o[i]; // 旧顶点索引
                n_vers.Add(o_vers[o_index] - basePos);
                n_nors.Add(o_nors[o_index]);
                n_uvs.Add(o_uvs[o_index]);
            }

            newmesh.SetVertices(n_vers);
            newmesh.SetNormals(n_nors);
            newmesh.SetUVs(0, n_uvs);
            newmesh.triangles = n_tris;

            return newmesh;
        }

        public static int GetSubMeshIndex(MeshRenderer render)
        {
            string[] lines = File.ReadAllLines(SceneManager.GetActiveScene().path);
            string name = render.gameObject.name;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("  m_Name:") && lines[i].Length > 10 && lines[i].Substring(10) == name)
                {
                    while (--i >= 0)
                    {
                        if (lines[i].StartsWith("  m_Component"))
                        {
                            while (i < lines.Length)
                            {
                                i++;
                                if (lines[i].StartsWith("  - component:"))
                                {
                                    string id = '&' + lines[i].Substring(24).Replace("}", "");
                                    for (int j = 0; j < lines.Length; j++)
                                    {
                                        if (lines[j].StartsWith("---") && lines[j].EndsWith(id) && lines[j + 1].StartsWith("MeshRenderer"))
                                        {
                                            while (j < lines.Length)
                                            {
                                                j++;
                                                if (lines[j] == "  m_StaticBatchInfo:")
                                                {
                                                    return int.Parse(lines[j + 1].Substring(18));
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            throw new EntryPointNotFoundException();
        }
    }
}
