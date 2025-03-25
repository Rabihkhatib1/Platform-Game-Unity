using System;
using System.Collections;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.UI;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 5;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        public int currentHP;

        public HealthBarUI m_HealthbarUI;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            m_HealthbarUI?.AnimateBar(true);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            m_HealthbarUI?.AnimateBar(false);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }

        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
        }

        // public void Awake()
        // {
        //     currentHP = maxHP;
        //     m_HealthbarUI = GetComponent<HealthBarUI>();
        //     if (m_HealthbarUI != null)
        //     {
        //         m_HealthbarUI.gameObject.SetActive(true);
        //         m_HealthbarUI.AnimateBarFull();
        //     }
        // }

        // public void Start()
        // {
        //     m_HealthbarUI = GetComponentInChildren<HealthBarUI>();

        // }

        public void Awake()
        {
            currentHP = maxHP;
            m_HealthbarUI = GetComponent<HealthBarUI>();
        }

        // private IEnumerator DelayRebuildUI()
        // {
        //     yield return null; // Wait one frame to ensure UIDocument is active
        //     m_HealthbarUI.RebuildUI();
        // }

        public void ResetHealth()
        {
            currentHP = maxHP;

            if (m_HealthbarUI == null)
                m_HealthbarUI = GetComponent<HealthBarUI>();

            if (m_HealthbarUI != null)
                m_HealthbarUI.ResetUI(); // Call your AnimateBarFull from here
        }


    }
}
