﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace opdemo
{
    public class SceneController : MonoBehaviour
    {
        // Singleton
        public static SceneController instance { get; private set; }
        private void Awake() { instance = this; }

        // Multi-person controlling
        [SerializeField] GameObject HumanPrefab;
        private Dictionary<int, CharacterAnimController> animControllers = new Dictionary<int, CharacterAnimController>();
        private AnimData frameData;
        public void PushNewFrameData(AnimData animData, float frameTime)
        {
            if (animData != null)
            {
                frameData = animData;
                List<int> animUpdated = new List<int>();
                foreach (AnimUnitData unitData in frameData.units)
                {
                    CharacterAnimController animController;
                    if (animControllers.TryGetValue(unitData.id, out animController)) // unit update
                    {
                        animController.PushNewUnitData(unitData, frameTime);
                        animUpdated.Add(unitData.id);
                    }
                    else // unit enter
                    {
                        if (HumanPrefab == null)
                        {
                            Debug.Log("No human prefab");
                            return;
                        } else
                        {
                            animControllers.Add(unitData.id, Instantiate(HumanPrefab).GetComponent<CharacterAnimController>());
                        }
                    }
                }
                foreach(var characterdata in animControllers)
                {
                    if (!animUpdated.Contains(characterdata.Key)) // unit exit
                    {
                        Destroy(characterdata.Value.gameObject);
                        animControllers.Remove(characterdata.Key);
                    }
                }
            }
        }

        // UI
        [SerializeField] Text StepNumberText;
        [SerializeField] GameObject InterpolationCheckmark;

        // Human and scene switch -- not in use
        //[SerializeField] List<GameObject> HumanModels;
        //[SerializeField] List<GameObject> SceneModels;
        //private int _HumanModelIndex = 0, _SceneModelIndex = 0;
        //public int HumanModelIndex { set { setHumanModel(value); } get { return _HumanModelIndex; } }
        //public int SceneModelIndex { set { setSceneModel(value); } get { return _SceneModelIndex; } }

        public CamFocusPart CamFocus = CamFocusPart.Hip;

        private int insertStepNum = 2;

        // Use this for initialization
        void Start()
        {
            InterpolationCheckmark.SetActive(CharacterAnimController.AllowInterpolation);
            /*if (HumanModels.Count == 0 || SceneModels.Count == 0)
            {
                Debug.Log("Empty list in Models or Scenes");
                return;
            }
            ResetAll();
            HumanModelIndex = 0;
            SceneModelIndex = 0;*/
        }

        /*private void ResetAll(bool human = true, bool scene = true)
        {
            if (human)
                foreach (GameObject o in HumanModels)
                {
                    o.SetActive(false);
                }

            if (scene)
                foreach (GameObject o in SceneModels)
                {
                    o.SetActive(false);
                }
        }

        // Models control
        public void NextScene()
        {
            SceneModelIndex++;
        }

        public void LastScene()
        {
            SceneModelIndex--;
        }

        public void NextHuman()
        {
            HumanModelIndex++;
        }

        public void LastHuman()
        {
            HumanModelIndex--;
        }*/

        public void Recenter()
        {
            // TODO
            //HumanModels[HumanModelIndex].GetComponent<CharacterAnimController>().Recenter();
        }

        public void Revertical()
        {
            // TODO
            //HumanModels[HumanModelIndex].GetComponent<CharacterAnimController>().Revertical();
        }

        // Anim control
        public void ToggleInterpolation()
        {
            CharacterAnimController.AllowInterpolation = !CharacterAnimController.AllowInterpolation;
            if (InterpolationCheckmark != null)
            {
                InterpolationCheckmark.SetActive(CharacterAnimController.AllowInterpolation);
            }
        }
        public void SetInsertStepNumber(int n)
        {
            insertStepNum = Mathf.Clamp(n, 1, 9);
            CharacterAnimController.InsertStepNumber = insertStepNum;
            StepNumberText.text = insertStepNum.ToString();
        }

        public void IncreaseStepNumber()
        {
            SetInsertStepNumber(insertStepNum + 1);
        }

        public void DecreaseStepNumber()
        {
            SetInsertStepNumber(insertStepNum - 1);
        }

        // Camera control
        public void SwitchFocus(int index)
        {
            CamFocus = (CamFocusPart)index;
            SetCameraFocus();
        }

        /*private void setHumanModel(int index)
        {
            if (index < 0)
            {
                setHumanModel(HumanModels.Count + index % HumanModels.Count);
                return;
            }
            else if (index >= HumanModels.Count)
            {
                setHumanModel(index % HumanModels.Count);
                return;
            }
            else
            {
                HumanModels[_HumanModelIndex].SetActive(false);
                _HumanModelIndex = index;
                HumanModels[_HumanModelIndex].SetActive(true);
                SetCameraFocus();
            }
        }

        private void setSceneModel(int index)
        {
            if (index < 0)
            {
                setSceneModel(SceneModels.Count + index % SceneModels.Count);
                return;
            }
            else if (index >= SceneModels.Count)
            {
                setSceneModel(index % SceneModels.Count);
                return;
            }
            else
            {
                SceneModels[_SceneModelIndex].SetActive(false);
                _SceneModelIndex = index;
                SceneModels[_SceneModelIndex].SetActive(true);
            }
        }*/

        private void SetCameraFocus()
        {
            // TODO
            //Transform center = HumanModels[_HumanModelIndex].GetComponent<CharacterAnimController>().GetFocusCenter(CamFocus);
            //if (center != null)
            //    CameraController.FocusCenter = center;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetInsertStepNumber(1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetInsertStepNumber(2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetInsertStepNumber(3);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetInsertStepNumber(4);
            }
            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetInsertStepNumber(5);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetInsertStepNumber(6);
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
            {
                SetInsertStepNumber(7);
            }
            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                SetInsertStepNumber(8);
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                SetInsertStepNumber(9);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Recenter();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Revertical();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchFocus(0);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                SwitchFocus(1);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchFocus(2);
            }
            /*
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                LastHuman();
            }
            if (Input.GetKeyDown(KeyCode.Period))
            {
                NextHuman();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                LastScene();
            }
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                NextScene();
            }*/
        }
    }

    public enum CamFocusPart
    {
        Hip, 
        Chest, 
        Head
    }
}
