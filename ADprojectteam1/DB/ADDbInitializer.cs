using ADprojectteam1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ADprojectteam1.DB
{
    public class ADDbInitializer<T> :DropCreateDatabaseAlways<ADDbContext> {
        protected override void Seed(ADDbContext context)
        {
            

            List<Item> items = new List<Item>();

            items.Add(new Item("C001","Clip","Clips Double 1\"",50,30,"Dozen"));
            items.Add(new Item("C002", "Clip", "Clips Double 2\"", 50, 30, "Dozen"));
            items.Add(new Item("C003", "Clip", "Clips Double 3/4\"", 50, 30, "Dozen"));
            items.Add(new Item("C004", "Clip", "Clips Paper Large", 50, 30, "Box"));
            items.Add(new Item("C005", "Clip", "Clips Paper Medium", 50, 30, "Box"));
            items.Add(new Item("C006", "Clip", "Clips Paper Small", 50, 30, "Box"));
            items.Add(new Item("E001", "Envelope", "Envelope Brown(3x6)", 600, 400, "Each"));
            items.Add(new Item("E002", "Envelope", "Envelope Brown(3x6) w/ Window", 600, 400, "Each"));
            items.Add(new Item("E003", "Envelope", "Envelope Brown(5x7)", 600, 400, "Each"));
            items.Add(new Item("E004", "Envelope", "Envelope Brown(5x7 w/ Window)", 600, 400, "Each"));
            items.Add(new Item("E020", "Eraser", "Eraser(hard)", 50, 20, "Each"));
            items.Add(new Item("E021", "Eraser", "Eraser(soft)", 50, 20, "Each"));
            items.Add(new Item("E030", "Exercise", "Exercise Book (100pg)", 100, 50, "Each"));
            items.Add(new Item("E031", "Exercise", "Exercise Book (120pg)", 100, 50, "Each"));
            items.Add(new Item("E032", "Exercise", "Exercise Book A4 Hardcover (100pg)", 100, 50, "Each"));
            items.Add(new Item("E033", "Exercise", "Exercise Book A4 hardcover (120pg)", 100, 50, "Each"));
            items.Add(new Item("E034", "Exercise", "Exercise Book A4 hardcover (200pg)", 100, 50, "Each"));
            items.Add(new Item("E035", "Exercise", "Exercise Book hardcover (100pg)", 100, 50, "Each"));
            items.Add(new Item("E036", "Exercise", "Exercise Book hardcover (120pg)", 100, 50, "Each"));
            items.Add(new Item("F020", "File", "File Separator", 100, 50, "Set"));
            items.Add(new Item("F021", "File", "File-Blue Plain", 200, 100, "Each"));
            items.Add(new Item("F022", "File", "File-Blue with Logo", 200, 100, "Each"));
            items.Add(new Item("F023", "File", "File-Brown w/o Logo", 200, 150, "Each"));
            items.Add(new Item("F024", "File", "File-Brown with Logo", 200, 150, "Each"));
            items.Add(new Item("F031", "File", "Folder Plastic Blue", 200, 150, "Each"));
            items.Add(new Item("F032", "File", "Folder Plastic Clear", 200, 150, "Each"));
            items.Add(new Item("F033", "File", "Folder Plastic Green", 200, 150, "Each"));
            items.Add(new Item("F034", "File", "Folder Plastic Pink", 200, 150, "Each"));
            items.Add(new Item("F035", "File", "Folder Plastic Yellow", 200, 150, "Each"));
            items.Add(new Item("H011", "Pen", "Highlighter Blue", 100, 80, "Box"));
            items.Add(new Item("H012", "Pen", "Highlighter Green", 100, 80, "Box"));
            items.Add(new Item("H013", "Pen", "Highlighter Pink", 100, 80, "Box"));
            items.Add(new Item("H014", "Pen", "Highlighter Yellow", 100, 80, "Box"));
            items.Add(new Item("H031", "Puncher", "Hole Puncher 2 holes", 50, 20, "Each"));
            items.Add(new Item("H032", "Puncher", "Hole Puncher 3 holes", 50, 20, "Each"));
            items.Add(new Item("H033", "Puncher", "Hole Puncher Adjustable", 50, 20, "Each"));
            items.Add(new Item("P010", "Pad", "Pad Postit Memo 1x2", 100, 60, "Packet"));
            items.Add(new Item("P011", "Pad", "Pad Postit Memo 1/2\"x1\"", 100, 60, "Packet"));
            items.Add(new Item("P012", "Pad", "Pad Postit Memo 1/2\"x2\"", 100, 60, "Packet"));
            items.Add(new Item("P013", "Pad", "Pad Postit Memo 2x3", 100, 60, "Packet"));
            items.Add(new Item("P014", "Pad", "Pad Postit Memo 2x4", 100, 60, "Packet"));
            items.Add(new Item("P015", "Pad", "Pad Postit Memo 3/4\"x2\"", 100, 60, "Packet"));
            items.Add(new Item("P016", "Pad", "Pad Postit Memo 3/4\"x4\"", 100, 60, "Packet"));
            items.Add(new Item("P020", "Paper", "Paper Photostat A3", 500, 500, "Box"));
            items.Add(new Item("P021", "Paper", "Paper Photostat A4", 500, 500, "Box"));
            items.Add(new Item("P030", "Pen", "Pen Ballpoint Black", 100, 50, "Dozen"));
            items.Add(new Item("P031", "Pen", "Pen Ballpoint Blue", 100, 50, "Dozen"));
            items.Add(new Item("P032", "Pen", "Pen Ballpoint Red", 100, 50, "Dozen"));
            items.Add(new Item("P033", "Pen", "Pen Felt Tip Black", 100, 50, "Dozen"));
            items.Add(new Item("P034", "Pen", "Pen Felt Tip Blue", 100, 50, "Dozen"));
            items.Add(new Item("P035", "Pen", "Pen Felt Tip Red", 100, 50, "Dozen"));
            items.Add(new Item("P036", "Pen", "Pen Transparency Permanent", 100, 50, "Packet"));
            items.Add(new Item("P037", "Pen", "Pen Transparency Soluble", 100, 50, "Packet"));
            items.Add(new Item("P038", "Pen", "Pen Whiteboard Marker Black", 100, 50, "Box"));
            items.Add(new Item("P039", "Pen", "Pen Whiteboard Marker Blue", 100, 50, "Box"));
            items.Add(new Item("P040", "Pen", "Pen Whiteboard Marker Green", 100, 50, "Box"));
            items.Add(new Item("P041", "Pen", "Pen Whiteboard Marker Red", 100, 50, "Box"));
            items.Add(new Item("P042", "Pen", "Pencil 2B", 100, 50, "Dozen"));
            items.Add(new Item("P043", "Pen", "Pencil 2B with Eraser End", 100, 50, "Dozen"));
            items.Add(new Item("P044", "Pen", "Pencil 4H", 100, 50, "Dozen"));
            items.Add(new Item("P045", "Pen", "Pencil B", 100, 50, "Dozen"));
            items.Add(new Item("P046", "Pen", "Pencil B with Eraser End", 100, 50, "Dozen"));
            items.Add(new Item("R001", "Ruler", "Ruler 6", 50, 20, "Dozen"));
            items.Add(new Item("R002", "Ruler", "Ruler 12", 50, 20, "Dozen"));
            items.Add(new Item("S100", "Scissors", "Scissors", 50, 20, "Each"));
            items.Add(new Item("S040", "Tape", "Scotch Tape", 50, 20, "Each"));
            items.Add(new Item("S041", "Tape", "Scotch Tape Despenser", 50, 20, "Each"));
            items.Add(new Item("S101", "Sharpener", "Sharpener", 50, 20, "Each"));
            items.Add(new Item("S010", "Shorthand", "Shorthand Book(100pg)", 100, 80, "Each"));
            items.Add(new Item("S011", "Shorthand", "Shorthand Book(120pg)", 100, 80, "Each"));
            items.Add(new Item("S012", "Shorthand", "Shorthand Book(60pg)", 100, 80, "Each"));
            items.Add(new Item("S020", "Stapler", "Stapler No.28", 50, 20, "Each"));
            items.Add(new Item("S021", "Stapler", "Stapler No.36", 50, 20, "Each"));
            items.Add(new Item("S022", "Stapler", "Stapler No.28", 50, 20, "Box"));
            items.Add(new Item("S023", "Stapler", "Stapler No.36", 50, 20, "Box"));
            items.Add(new Item("T001", "Tacks", "Thumb Tacks Large", 10, 10, "Box"));
            items.Add(new Item("T002", "Tacks", "Thumb Tacks Medium", 10, 10, "Box"));
            items.Add(new Item("T003", "Tacks", "Thumb Tacks Small", 10, 10, "Box"));
            items.Add(new Item("T020", "Tparency", "Transparency Blue", 100, 200, "Box"));
            items.Add(new Item("T021", "Tparency", "Transparency Clear", 500, 400, "Box"));
            items.Add(new Item("T022", "Tparency", "Transparency Green", 100, 200, "Box"));
            items.Add(new Item("T023", "Tparency", "Transparency Red", 100, 200, "Box"));
            items.Add(new Item("T024", "Tparency", "Transparency Reverse Blue", 100, 200, "Box"));
            items.Add(new Item("T025", "Tparency", "Transparency Cover 3M", 500, 400, "Box"));
            items.Add(new Item("T100", "Tray", "Trays In/Out", 20, 10, "Set"));





            foreach (Item i in items)
                context.Item.Add(i);


            List<Department> deps = new List<Department>();

            deps.Add(new Department("English Dept", "ENGL", "8742234", "8921456", "Stationery Store", 5, 3));
            deps.Add(new Department("Computer Science", "CPSC", "8901235", "8921457", "Stationery Store", 5, 3));
            deps.Add(new Department("Commerce Dept", "COMM", "8741284", "8921256", "Stationery Store", 5, 3));
            deps.Add(new Department("Registrar Dept", "REGR", "8901266", "8921465", "Stationery Store", 5, 3));
            deps.Add(new Department("Zoology Dep", "ZOOL", "8901266", "8921456", "Stationery Store", 5, 3));

            foreach (Department d in deps)
                context.Department.Add(d);


            List<Employee> emps = new List<Employee>();

            emps.Add(new Employee("depmanager", "dadb22fff1accc6fe4aa720910d8a9a1", "DepManager", "11233", "Mr", "Kaung Khant Kyaw", "k3@gmail.com"));
            emps.Add(new Employee("storeclerk", "cfc6722c02fd40116a5dd21ed60f9352", "StoreClerk", "11234", "Mr", "GuoMing", "gm@gmail.com"));
            emps.Add(new Employee("storemanager", "4e02b0e68f049d9fd40b2013f7b637e2", "StoreManager", "11235", "Ms", "Aye", "aye@gmail.com"));
            emps.Add(new Employee("storesup", "8050b9c976aed3e1ab492f373fbd8421", "StoreSup", "11236", "Ms", "Suwetaa", "suwetaa@gmail.com"));
            emps.Add(new Employee("depemp", "bb3852c905fe1d884ad105f9b6dcbc19", "DepEmp", "11238", "Ms", "Jenny", "jenny@gmail.com"));
            emps.Add(new Employee("deprep", "7706011852e262a2d5012ace5b77c80e", "DepRep", "11239", "Mr", "Than", "than@gmail.com"));
            emps.Add(new Employee("depemp1", "7706011852e262a2d5012ace5b77c80e", "DepEmp", "11240", "Mr", "Than", "than@gmail.com"));
            emps.Add(new Employee("depmanager1", "7706011852e262a2d5012ace5b77c80e", "DepManager", "11241", "Mr", "Than", "than@gmail.com"));

            foreach (Employee e in emps)
                context.Employee.Add(e);
            
            


            

            emps[0].department = deps[0];
            emps[4].department = deps[0];
            emps[5].department = deps[0];
            emps[6].department = deps[1];
            emps[7].department = deps[1];


            List<Supplier> sups = new List<Supplier>();

            sups.Add(new Supplier("ALPA","ALPHA Office Supplies","Ms Irene Tan", "Blk 1128, Ang Mo Kio Industrial Park #02-1108 Ang Mo Kio Street 62 Singapore 622262","4619928","4612238","alpha@gmail.com"));
            sups.Add(new Supplier("CHEP", "Cheap Stationer", "Mr Soh Kway Koh", "Blk 34, Clementi Road #07-02 Ban Ban Soh Building Singapore 110525", "3543234", "4742434", "chep@gmail.com"));
            sups.Add(new Supplier("BANE", "BANES Shop", "Mr Loh Ah Pek", "Blk124, Alexandra Road, #03-04 Banes Building Singapore 550315", "4781234", "4792434", "banes@gmail.com"));
            sups.Add(new Supplier("OMEG", "OMEGA Stationery Suppier", "Mr Ronnie Ho", "Blk11, Hillview Avenue #03-04, Singapore 6790360", "7671233", "7671234", "omega@gmail.com"));

            foreach (Supplier s in sups)
                context.Supplier.Add(s);

            List<ItemSupplier> itemsup1 = new List<ItemSupplier>();
            for (int i = 0; i < 85; i++) itemsup1.Add(new ItemSupplier(items[i], sups[1], 1.00));
            List<ItemSupplier> itemsup2 = new List<ItemSupplier>();
            for (int i = 0; i < 85; i++) itemsup2.Add(new ItemSupplier(items[i], sups[2], 2.00));
            List<ItemSupplier> itemsup3 = new List<ItemSupplier>();
            for (int i = 0; i < 85; i++) itemsup3.Add(new ItemSupplier(items[i], sups[3], 3.00));





            List<ReqItem> rit = new List<ReqItem>();
            rit.Add(new ReqItem(items[1],emps[4],10));
            rit.Add(new ReqItem(items[68], emps[4], 45));
            rit.Add(new ReqItem(items[40], emps[4], 100));
            rit.Add(new ReqItem(items[84], emps[4], 25));
            rit.Add(new ReqItem(items[73], emps[4], 55));
            

            foreach (ReqItem ri in rit)
                context.ReqItem.Add(ri);

            List<ReqItem> rit1 = new List<ReqItem>();
            rit1.Add(new ReqItem(items[1], emps[5], 10));
            rit1.Add(new ReqItem(items[68], emps[5], 45));
            rit1.Add(new ReqItem(items[40], emps[5], 100));
            rit1.Add(new ReqItem(items[84], emps[5], 25));
            rit1.Add(new ReqItem(items[73], emps[5], 55));


            foreach (ReqItem ri in rit1)
                context.ReqItem.Add(ri);

            SRequisition srq = new SRequisition();
            srq.ListItem = rit;
            context.SRequisition.Add(srq);

            SRequisition srq1 = new SRequisition();
            srq1.ListItem = rit1;
            context.SRequisition.Add(srq1);

           
            
            
            /*
            List<ReOrderRecord> lro = new List<ReOrderRecord>();
            lro.Add(new ReOrderRecord(itemsup[0]));
            lro.Add(new ReOrderRecord(itemsup[1]));
            lro.Add(new ReOrderRecord(itemsup[3]));

            foreach (ReOrderRecord rr in lro)
                context.ReOrderRecord.Add(rr);

            List<ReOrderRecord> lro1 = new List<ReOrderRecord>();
            lro.Add(new ReOrderRecord(itemsup[0]));
            lro.Add(new ReOrderRecord(itemsup[1]));
            lro.Add(new ReOrderRecord(itemsup[3]));

            foreach (ReOrderRecord rr in lro1)
                context.ReOrderRecord.Add(rr);

            PurchaseOrder po = new PurchaseOrder(lro);
            PurchaseOrder po1 = new PurchaseOrder(lro1);
            context.PurchaseOrder.Add(po);
            context.PurchaseOrder.Add(po1);*/

            List<StockCard> lsc = new List<StockCard>();
            DateTime dt = DateTime.Today;
            
            lsc.Add(new StockCard(items[58],dt,1,500,550));
            lsc.Add(new StockCard(items[58], dt, deps[0], -20, 530));
            lsc.Add(new StockCard(items[58], dt, 4, 534));
            lsc.Add(new StockCard(items[58], dt, deps[1], -30, 504));
            lsc.Add(new StockCard(items[58], dt, deps[2], -50, 454));
            lsc.Add(new StockCard(items[58], dt, 1, 500, 954));

            foreach (StockCard sc in lsc)
                context.StockCard.Add(sc);


            List<StockCard> lsc1 = new List<StockCard>();
            
            for(int i=0; i<85;i++)
            lsc1.Add(new StockCard(items[i], dt, 1, 500, 550));

            foreach (StockCard sc in lsc1)
                context.StockCard.Add(sc);
            

            foreach (StockCard sc in lsc)
                context.StockCard.Add(sc);

            List<InventoryAdj> liva = new List<InventoryAdj>();
            liva.Add(new InventoryAdj(items[0],-6,"Broken items"));
            liva.Add(new InventoryAdj(items[1], 3, "Free gift in offer pack"));
            
            foreach(InventoryAdj ia in liva)
                context.InventoryAdj.Add(ia);

            

            base.Seed(context);

            foreach (ItemSupplier x in itemsup1)
                context.ItemSupplier.Add(x);
            foreach (ItemSupplier x in itemsup2)
                context.ItemSupplier.Add(x);
            foreach (ItemSupplier x in itemsup3)
                context.ItemSupplier.Add(x);


        } 
    }
    
    
}