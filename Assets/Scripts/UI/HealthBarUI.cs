using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer.UI
{
    /// <summary>
    /// A controller for the player healthbar UI.
    /// </summary>
    public class HealthBarUI : MonoBehaviour
    {
        public Transform TransformToFollow;

        private VisualElement m_Bar;
        private Camera m_MainCamera;
        private VisualElement[] m_Hearts;

        private void Start()
        {
            // m_MainCamera = Camera.main;
            // m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
            // m_Hearts = m_Bar.Children().ToArray(); //new

            // // SetPosition();

            m_MainCamera = Camera.main;
            m_Bar = GetComponent<UIDocument>().rootVisualElement.Q("Container");
            m_Hearts = m_Bar.Children().ToArray();
            // AnimateBarFull(); // safe here
        }

        public void AnimateBar(bool increaseHealth)
        {

            if (increaseHealth)
            {
                VisualElement nextHeart = m_Hearts.Where(x => !x.visible).FirstOrDefault();
                nextHeart.style.visibility = Visibility.Visible;

            }
            else
            {
                VisualElement nextHeart = m_Hearts.Where(x => x.visible).LastOrDefault();
                nextHeart.style.visibility = Visibility.Hidden;

            }
        }

        public void AnimateBarFull()
        {
            for (int i = 0; i < m_Hearts.Length; i++)
            {
                var nextHeart = m_Hearts.Where(x => !x.visible).FirstOrDefault();
                if (nextHeart != null)
                    nextHeart.style.visibility = Visibility.Visible;
            }
        }

        public void ResetUI()
        {
           var doc = GetComponent<UIDocument>();
           if (doc == null)
           {
               Debug.LogError("UIDocument is missing");
               return;
           }

           m_Bar = doc.rootVisualElement.Q("Container");
           if (m_Bar == null)
           {
               Debug.LogError("Could not find Container in UIDocument");
               return;
           }

           m_Hearts = m_Bar.Children().ToArray();
           AnimateBarFull();
        }

    }
}