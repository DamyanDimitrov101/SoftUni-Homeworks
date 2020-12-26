using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Races.Contracts;
using MXGP.Models.Riders;
using MXGP.Models.Riders.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        private List<T> models;
        private Type currentType;

        public Repository()
        {
            this.models = new List<T>();
        }

        public void Add(T model)
        {
            if (model!=null)
            {
                this.models.Add(model);
                this.currentType = model.GetType();
            }
        }

        public IReadOnlyCollection<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetByName(string name)
        {
            T model = default;
            if (currentType is IRider)
            {
                model = this.models.First(r => (r as IRider).Name == name);
            }

            if (currentType is IMotorcycle)
            {
                model = this.models.First(m=>(m as IMotorcycle).Model==name);
            }

            if (currentType is IRace)
            {
                model = this.models.First(m => (m as IRace).Name == name);
            }

            return model;
        }

        public bool Remove(T model)
        {
            return this.models.Remove(model);
        }
    }
}
