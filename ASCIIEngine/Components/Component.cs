using ASCIIEngine.Core.BasicClasses;

namespace ASCIIEngine.Core.Components
{
    public abstract class Component
    {
        protected readonly GameObject Parent;

        protected Component(GameObject parent)
        {
            Parent = parent;
        }

        protected Component()
        {
            
        }

        internal virtual void Update(){}
    }
}