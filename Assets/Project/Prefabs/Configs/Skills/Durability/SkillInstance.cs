namespace Project.Prefabs.Configs.Skills.Durability
{
    public abstract class SkillInstance
    {
        private SkillHolder _skillHolder;
        
        protected SkillInstance(SkillHolder skillHolder) => _skillHolder = skillHolder;
        
        public abstract void Disable();
    }
}