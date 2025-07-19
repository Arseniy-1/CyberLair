using System.Collections.Generic;
using System.Linq;
using Project.Scripts.MessageBroker;
using UnityEngine;

namespace Project.Scripts.SkillSystem
{
    public class SkillUIHandler
    {
        private readonly GameObject _gameUI;
        private readonly SkillSelector _skillSelector;

        public SkillUIHandler(GameObject gameUI, SkillSelector skillSelector)
        {
            _gameUI = gameUI;
            _skillSelector = skillSelector;
        }

        public void ShowSkillSelection(IReadOnlyList<Skill> skills, int inputCount, int outputCount)
        {
            if (skills.Any() == false)
                return;

            MessageBrokerHolder.Game
                .Publish(new M_GamePaused());
        
            _gameUI.SetActive(false);
        
            _skillSelector.ShowSkills(skills, inputCount, outputCount);
        }

        public void CloseSkillSelection()
        {
            MessageBrokerHolder.Game
                .Publish(new M_GameUnpaused());
        
            _gameUI.SetActive(true);
        }
    }
}