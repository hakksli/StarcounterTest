using Starcounter;

namespace KitchenSink
{
    partial class SortableList : Page, IBound<People>
    {
       protected override void OnData()
        {
            base.OnData();
            if (Exists())
            {
                Db.Transact(() => {
                    DeleteAll();
                });
            }
                        
                Db.Transact(() =>
                {
                    new People() {Name = "Zlatan", Id = 1, Position = 1};
                    new People() {Name = "Yaya", Id = 2, Position = 2};
                    new People() {Name = "John", Id = 3, Position = 3};
                });          
                     
        }

        public static bool Exists()
        {
            var exists = Db.SQL<People>("SELECT p FROM People p FETCH ?", 1).First;
            if (exists == null)
            {
                return false;
            }
            return true;
        }

        public static void DeleteAll()
        {
            Db.SlowSQL("DELETE FROM People");
        }
    }
}