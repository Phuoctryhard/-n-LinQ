using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class Product
    {
        public int ID { set; get; }
        public string Name { set; get; }         // tên
        public double Price { set; get; }        // giá
        public string[] Colors { set; get; }     // các màu sắc
        public int Brand { set; get; }           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
           => $"{ID,4} {Name,12} {Price,5} {Brand,2} {string.Join(",", Colors)}";

    }
    // danh sach thuong hieu
    public class Brand
    {
        public string Name { set; get; }
        public int ID { set; get; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            /*         Product a = new Product(1,"ABC",1000,new string[] {"xanh","Do"},2);
                     Console.WriteLine(a);*/
            var brands = new List<Brand>() {
            new Brand{ID = 1, Name = "Công ty AAA"},
            new Brand{ID = 2, Name = "Công ty BBB"},
            new Brand{ID = 4, Name = "Công ty CCC"},
            };

            var products = new List<Product>()
            {
            new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
            new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
            new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
            new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
             new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
            new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
            new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };
            // lấy sản pham gia 400
            var sanpham = from sp in products
                          where sp.Price == 400
                          select sp;
            // sanpham co kieu du lieu IEnumerable : 
            // kq trả về tập hợp luu trong sanpham
            // 
            foreach (var sp in sanpham)
            {
                Console.WriteLine(sp);
            }
            // trong list products
            //select 
            // khai bao delegate
            // bieu thuc lamda 

            // với mỗi sản phẩm p thuộc sản phẩm 
            // ket qua luu vao 1 tap hop 

            var xxx = products.Select(p => p.Name + " " + p.Price);
            var xxxx = products.Select((p) => { return p.Name + " " + p.ID; });
            foreach (var s in xxx)
            {
                Console.WriteLine(s);
            }

            // trả về 1 đối tượng vô danh 
            // select tra ve 1 tap hop ma 1 phan tu trong tap hop tra ve 1 mang chuoi 
            // select many  bien kq là ttapj hop chuoi  
            var y = products.Select(p => new
            {
                Name = p.Name,
                ID = p.ID,
                Price = p.Price,
            });
            foreach (var y0 in y)
            {
                Console.WriteLine(y0);
            }
            // WHERE
            // Return true / false
            var x = products.Where(p => p.ID == 1);
            foreach (var s in x)
            {
                Console.WriteLine(s);
            }
            // min . max. average 
            int[] number = { 1, 2, 3, 4, 5 };
            Console.WriteLine(number.Where(p => p % 2 == 0).Max());
            // tinh trung binh
            Console.WriteLine(number.Average(p => p));
            // joint , kết hợp nguồn dữ liệu lấy ra dữ liêu 
            //  tham so 1 : bảng ket hop ,
            //  tham so 2: chỉ ra dữ liệu nào trong product kết hợp 
            // tham số 3 : dư lieu nao trong brands để kết hợp 
            // tham só 4 : kết quả lấy ra đưa vào delegate (p,b)
            // mỗi phần tử ở product lấy dữ liệu brand , 

            var kethop = products.Join(brands, p => p.Brand, c => c.ID, (p, c) =>
            {
                return new
                {
                    Ten = p.Name,
                    Thuonghieu = p.Brand,
                    p.Price
                };
            });
            foreach (var item in kethop)
            {
                Console.WriteLine(item);
            }

            // group join 
            // phan tu tra ve 1 nhom, theo 1 nhom ban dau
            // với mỗi thương hiệu liệt kê sản phẩm thuoc thương hiệu
            // ts1: nguồn nằm trong nhóm , 
            // nhung brand thuoc products  = b.id sẽ đưa vào 1 nhòm 
            //tham so cuoi : nhãn hiệu lấy ra tạ nhóm , thám só tiếp
            //tập hợp phần tử thuộc nhóm 
            var kq = brands.GroupJoin(products, d => d.ID, p => p.Brand, (brand, pros) =>
            {
                return new
                {
                    thuonghieu = brand.Name,
                    Cacsanpham = pros
                };
            });
            foreach (var xx in kq)
            {
                Console.WriteLine("Thuong hieu " + xx.thuonghieu);
                foreach (var sp in xx.Cacsanpham)
                {
                    Console.WriteLine("San Pham " + sp);
                }
            }
            // take // lấy ra 3 san pham dau 
            products.Take(3).ToList().ForEach(p => Console.WriteLine(p));
            // skip bo qua 1 phan tu dau tien , lấy phần từ sau nó 
            products.Skip(1).ToList().ForEach(p => Console.WriteLine(p));
            // order by 
            products.OrderBy(p => p.Name).ToList().ForEach(p => Console.WriteLine(p));
            // same reaverse 
            // group by : nhóm phần tử lại theo nhóm 
            // nhóm theo thương hiệu , or giá 
            var nhomthuonghieu = products.GroupBy(p => p.Price);
            foreach (var thuonghieu in nhomthuonghieu)
            {
                Console.WriteLine(thuonghieu.Key);
                foreach (var p in thuonghieu)
                {
                    Console.WriteLine(p);
                }

            }
            // distinc 
            // su dung select sẽ lỗi vì no trả về 1 mảng chuỗi , we đang cần trả về chuỗi 

            products.SelectMany(p => p.Colors).Distinct().ToList().ForEach(mau => Console.WriteLine(mau));
            Console.WriteLine("San pham Gia < 500 and mau xanh ");

            // Gia < 500 and mau xanh 
            var qr = from product in products
                     from color in product.Colors
                     where product.Price <= 400 && color.Equals("Xanh")
                     select new
                     {
                         ten = product.Name,
                         Gia = product.Price,
                         Cacmau = product.Colors,
                     };
            qr.ToList().ForEach(p =>
            {
                Console.WriteLine($"{p.ten} - {p.Gia}");
                Console.WriteLine(string.Join(" ", p.Cacmau));
                Console.WriteLine($" {p.ten} ");

            });
            // Sản phẩm Giá < 500 và màu xanh 



            Console.WriteLine("San pham Gia < 500 and mau xanh lần 2 ");
            var sps = from product in products
                          // trong phan tu co nhieu mau sac (list color)


                      where product.Price < 500 /*&& product.Colors.Equals("Xanh")*/
                      select new
                      {
                          ten = product.Name,
                          Gia = product.Price
                      };
            foreach (var i in sps.ToList())
            {
                Console.WriteLine(i.ten + " " + i.Gia);
            }

            // nhom san pham theo gia 
            // Nhomsptheogia : tập hợp list nhómspgiá 
            // trong Nhomsptheogia có list phần tử 
            // trong list phan tu có những phần tử 

            Console.WriteLine(" San Pham theo gia ");
            var Nhomsptheogia = from p in products
                                group p by p.Price;// phan tu nhom theo gia
            Nhomsptheogia.ToList().ForEach(group =>
            {
                Console.WriteLine(group.Key); // key chinh la gia
                group.ToList().ForEach(p => Console.WriteLine(p));
            });


            // sort theo Nhóm giá
            Console.WriteLine(" Sort San Pham theo gia ");
            var Nhomsptheogia1 = from p in products
                                 group p by p.Price into grr
                                 orderby grr.Key// phan tu nhom theo gia va luu vao nhom gr
                                 select grr;//
            Nhomsptheogia1.ToList().ForEach(group =>
            {
                Console.WriteLine(group.Key); // key chinh la gia
                group.ToList().ForEach(p => Console.WriteLine(p));
            });

            // truy vấn trả về đối tượng 
            // Doi tuong :
            // gia 
            // cacsanpham 
            // so luong
            var Nhomsptheogia2 = from p in products
                                 group p by p.Price into grr
                                 orderby grr.Key// phan tu nhom theo gia va luu vao nhom gr
                                 let sl = grr.Count()
                                 select new
                                 {
                                     Gia = grr.Key,
                                     cacsanpham = grr.ToList(),
                                     Soluong = sl
                                 };//
            Nhomsptheogia2.ToList().ForEach(ii =>
            {
                Console.WriteLine(ii.Gia);
                Console.WriteLine(ii.Soluong);// key chinh la gia
                ii.cacsanpham.ForEach(p => Console.WriteLine(p));
            });


            // join : kết hợp 2 nguon du lieu 
            var b = from Product in products
                    join moiphantu in brands /*ket noi = on */ on Product.Brand equals moiphantu.ID
                    select new
                    {
                        ten = Product.Name,
                        gia = Product.Price,

                    };
            b.ToList().ForEach(o=> Console.WriteLine(o.ten +" "+o.gia));
        }
        // còn nữa 

    }
}
// IEnumerable<T> (Array,List,Stack,queue..)
// 1 xác đinh nguon from tenphantu in Ienumerables
   // ... where , order by  , let tenbien  =??
   //2 lay du lieu : select , group by 


