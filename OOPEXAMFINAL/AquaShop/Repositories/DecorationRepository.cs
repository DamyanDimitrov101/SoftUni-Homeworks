using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> models;

        public DecorationRepository()
        {
            this.models = new List<IDecoration>();
        }

        public IReadOnlyCollection<IDecoration> Models => this.models.AsReadOnly();

        public void Add(IDecoration model)
        {
            if (model!=null)
            {
                this.models.Add(model);
            }
        }

        public IDecoration FindByType(string type)
        {
            IDecoration decoration = null;
            Type typeDecoration = Assembly.GetCallingAssembly().GetTypes().First(t=>t.Name == type);

            decoration = this.models.First(d=>d.GetType()==typeDecoration);

            return decoration;
        }

        public bool Remove(IDecoration model)
        {
            return this.models.Remove(model);
        }
    }
}
