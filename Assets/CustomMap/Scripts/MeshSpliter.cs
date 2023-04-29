#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/09

using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Torappu.Battle;
using CustomMap.TileInformation;
using static Spine.Unity.Editor.SkeletonBaker.BoneWeightContainer;

namespace CustomMap
{
    /// <summary>
    /// 切割静态网格体
    /// </summary>
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
            GameObject[] selectedObjs = Selection.gameObjects;
            if (selectedObjs.Length == 0) return;
            try
            {
                meshRenderers.Clear();
                mainMeshRenderer = null;
                foreach (GameObject obj in selectedObjs)
                {
                    Tile _tile = obj?.GetComponent<TileGraphic>()?._tile;
                    MeshRenderer _render = obj?.GetComponent<MeshRenderer>();
                    if (_tile || !mainMeshRenderer)
                    {
                        tile = _tile;
                        mesh = obj?.GetComponent<MeshFilter>()?.sharedMesh;
                        mainMeshRenderer = _render;
                    }
                    if (_render)
                        meshRenderers.Add(_render);
                }
                prefabName = tile?._tileKey;
            }
            catch (MissingComponentException) { }
        }

        private static Action m_onSelectionChanged;

        [SerializeField]
        private Mesh mesh;
        [SerializeField]
        private MeshRenderer mainMeshRenderer;
        [SerializeField]
        private HashSet<MeshRenderer> meshRenderers = new HashSet<MeshRenderer>();
        [SerializeField]
        private Tile tile;
        [SerializeField]
        private string prefabName;

        [Button("切割选中物体并生成预制体")]
        private void Split(string path = "CustomMap/Mesh", string prefabPath = "CustomMap/TilePrefabs", bool generatePrefab = true)
        {
            if (meshRenderers.Count == 0) return;

            Dictionary<MeshRenderer, Mesh> render2Mesh = new Dictionary<MeshRenderer, Mesh>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                Mesh meshSplited = SplitFrom(mesh, meshRenderer, meshRenderer.transform.position);
                render2Mesh.Add(meshRenderer, meshSplited);
            }

            DirectoryInfo meshDir = new DirectoryInfo(Path.Combine(Application.dataPath, path));
            if (!meshDir.Exists) meshDir.Create();
            foreach (KeyValuePair<MeshRenderer, Mesh> pair in render2Mesh)
            {
                AssetDatabase.CreateAsset(pair.Value, $"Assets/{path}/{pair.Key.name}.asset");
            }

            if (!generatePrefab) return;
            if (tile != null)
            {
                GameObject prefab = new GameObject(prefabName);
                GameObject tilePrefab = Instantiate(tile.gameObject, new Vector3(0f, 0f, -tile._height), Quaternion.Euler(Vector3.zero), prefab.transform);
                tilePrefab.transform.localScale = Vector3.one;
                tilePrefab.name = tile._tileKey;

                // 处理主Mesh
                GameObject mainMeshPrefab = Instantiate(mainMeshRenderer.gameObject, new Vector3(0f, 0f, mainMeshRenderer.transform.position.z), Quaternion.Euler(Vector3.zero), prefab.transform);
                mainMeshPrefab.transform.localScale = Vector3.one;
                mainMeshPrefab.name = render2Mesh[mainMeshRenderer].name;
                mainMeshPrefab.GetComponent<MeshFilter>().mesh = render2Mesh[mainMeshRenderer];
                TileGraphic mainTileGraphic = mainMeshPrefab.GetComponent<TileGraphic>(); // 处理TileGraphic
                if (mainTileGraphic)
                {
                    mainTileGraphic._tile = null;
                    mainTileGraphic._gridPos = new Torappu.GridPosition { col = 0, row = 0 };
                }

                Vector3 rootPos = mainMeshRenderer.transform.position;
                foreach (KeyValuePair<MeshRenderer, Mesh> pair in render2Mesh)
                {
                    if (pair.Key == mainMeshRenderer) continue; // 如果是主Mesh，跳过

                    Debug.Log(pair.Key.transform.position);
                    Debug.Log(rootPos);
                    // 处理附加MeshPrefab
                    GameObject meshPrefab = Instantiate(pair.Key.gameObject, mainMeshPrefab.transform);
                    meshPrefab.transform.localPosition = pair.Key.transform.position - rootPos;
                    meshPrefab.transform.localEulerAngles = Vector3.zero;
                    meshPrefab.transform.localScale = Vector3.one;
                    meshPrefab.name = pair.Key.name;
                    meshPrefab.GetComponent<MeshFilter>().mesh = pair.Value;

                    TileGraphic tileGraphic = meshPrefab.GetComponent<TileGraphic>(); // 处理TileGraphic
                    if (tileGraphic)
                    {
                        tileGraphic._tile = null;
                        tileGraphic._gridPos = new Torappu.GridPosition { col = 0, row = 0 };
                    }

                }

                TileDataPair cusTile = prefab.AddComponent<TileDataPair>();
                cusTile.tile = tilePrefab.GetComponent<Tile>();
                cusTile.tile._graphic = null;
                cusTile.tile._allGraphicList.Clear();
                cusTile.tileGraphic = mainMeshPrefab;

                DirectoryInfo prefabDir = new DirectoryInfo(Path.Combine(Application.dataPath, prefabPath));
                if (!prefabDir.Exists) prefabDir.Create();
                Selection.activeObject = PrefabUtility.CreatePrefab($"Assets/{prefabPath}/{prefabName}.prefab", prefab);
                DestroyImmediate(prefab);
            }
            else
            {
                // 处理主Mesh
                GameObject mainMeshPrefab = Instantiate(mainMeshRenderer.gameObject, Vector3.zero, Quaternion.Euler(Vector3.zero));
                mainMeshPrefab.transform.localScale = Vector3.one;
                mainMeshPrefab.name = render2Mesh[mainMeshRenderer].name;
                mainMeshPrefab.GetComponent<MeshFilter>().mesh = render2Mesh[mainMeshRenderer];

                Vector3 rootPos = mainMeshRenderer.transform.position;
                foreach (KeyValuePair<MeshRenderer, Mesh> pair in render2Mesh)
                {
                    if (pair.Key == mainMeshRenderer) continue; // 如果是主Mesh，跳过

                    // 处理附加MeshPrefab
                    GameObject meshPrefab = Instantiate(pair.Key.gameObject, mainMeshPrefab.transform);
                    meshPrefab.transform.localPosition = pair.Key.transform.position - rootPos;
                    meshPrefab.transform.localEulerAngles = Vector3.zero;
                    meshPrefab.transform.localScale = Vector3.one;
                    meshPrefab.name = pair.Key.name;
                    meshPrefab.GetComponent<MeshFilter>().mesh = pair.Value;
                }

                DirectoryInfo prefabDir = new DirectoryInfo(Path.Combine(Application.dataPath, prefabPath));
                if (!prefabDir.Exists) prefabDir.Create();
                Selection.activeObject = PrefabUtility.CreatePrefab($"Assets/{prefabPath}/{prefabName}.prefab", mainMeshPrefab);
                DestroyImmediate(mainMeshPrefab);
            }
        }

        public static Mesh SplitFrom(Mesh mesh, MeshRenderer render, Vector3 basePos)
        {
            HashSet<int> index = new HashSet<int>();
            return SplitFrom(mesh, new int [1] { GetSubMeshIndex(render) }, basePos);
        }

        public static Mesh SplitFrom(Mesh mesh, int[] subMeshIndexs, Vector3 basePos)
        {
            Debug.Log("Spliting subMeshIndex: " + string.Join(", ", subMeshIndexs));
            Mesh newmesh = new Mesh();

            List<Vector3> o_vers = new List<Vector3>(); // 旧顶点数据
            List<Vector3> o_nors = new List<Vector3>(); // 旧法向数据
            List<Vector2> o_uvs = new List<Vector2>(); // 旧UV数据

            // 从模板mesh获得数据
            mesh.GetVertices(o_vers);
            mesh.GetNormals(o_nors);
            mesh.GetUVs(0, o_uvs); // UV通道1 贴图

            List<int> n2o = new List<int>(); // 新索引到旧索引的映射列表

            List<int> o_tris = new List<int>(); // 旧的三角形顶点索引
            for (int i = 0; i < subMeshIndexs.Length; i++)
            {
                o_tris.AddRange(mesh.GetTriangles(subMeshIndexs[i]));
            }

            int[] n_tris = new int[o_tris.Count]; // 新的三角形顶点索引

            for (int i = 0; i < o_tris.Count; i++) // 遍历三角形顶点
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
            if (!render.isPartOfStaticBatch)
            {
                Debug.LogWarning($"The {render} isn't a part of static batch");
                return -1;
            }

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
#endif