
using System.Collections.Generic;
using System.Linq;
using w1.Models;

namespace w1.Service
{
    public class GoodsService
    {
        DefaultContext db;
        public GoodsService(DefaultContext database)
        {
            db = database;
        }

        public Nomenclature GetNomenclatureById(int? id)
        {
            Nomenclature nomenclature = db.Goods.Find(id);
            return nomenclature;
        }

        public Nomenclature CreateNomenclature()
        {
            Nomenclature newnomenclature = new Nomenclature();
            return newnomenclature;
        }

        public void DeleteNomenclature(Nomenclature nomenclature)
        {
            db.Goods.Remove(nomenclature);
            try
            {
                db.SaveChanges(); ;
            }
            catch { }
        }

        public void AddSaveNomenclature(Nomenclature nomenclature)
        {
            if (nomenclature.Id == 0)
            {
                db.Goods.Add(nomenclature);
            }
            else
            {
                db.Entry(nomenclature).State = System.Data.Entity.EntityState.Modified;
            }
            try
            {
                db.SaveChanges();
            }
            catch { }
        }

        public List<Nomenclature> GetAllGoods()
        {
            return db.Goods.ToList();
        }
    }
}