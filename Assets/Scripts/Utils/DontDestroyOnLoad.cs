﻿// ©2019 - 2023 FROLICODE TECHNOLOGIES PRIVATE LTD
// All rights reserved
// Redistribution of this software is strictly not allowed.
// Copy of this software can be obtained from unity asset store only.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Frolicode
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static Dictionary<string, DontDestroyOnLoad> instances = new Dictionary<string, DontDestroyOnLoad>();
        private bool isInitialLoad = true;
        private bool carriedToGameplay = false;
        private void Awake()
        {
            // If there's no instance with this object's name, make this the instance and make it persistent
            if (!instances.ContainsKey(gameObject.name))
            {
                DontDestroyOnLoad(gameObject);
                instances[gameObject.name] = this;
            }
            else
            {
                // If there's already an instance with this object's name, and it's not this one, destroy this
                if (instances[gameObject.name] != this)
                {
                    Destroy(gameObject);
                    return;
                }
            }

            // Subscribe to the scene loaded event
            // SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // If it's the initial load, just set the flag to false and return
            if (isInitialLoad)
            {
                isInitialLoad = false;
                return;
            }
            //
            // if (scene.name == "GameplaySceneName") // Replace "GameplaySceneName" with your gameplay scene's name
            // {
            //     carriedToGameplay = true;
            // }

            // Check if the loaded scene is the main scene (replace "MainSceneName" with your main scene's name)
            if (scene.name == "MainMenu")
            {
                // If we're loading the main scene, destroy the current instance with this object's name
                if (instances.ContainsKey(gameObject.name)  && instances[gameObject.name] == this)
                {
                    Destroy(gameObject);
                    instances.Remove(gameObject.name);
                }
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from the scene loaded event to avoid memory leaks
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
