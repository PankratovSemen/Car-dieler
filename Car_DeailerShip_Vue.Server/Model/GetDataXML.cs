using Car_DeailerShip_Vue.Server.Model.Database;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Car_DeailerShip_Vue.Server.Model
{
    public class GetDataXML
    {
        public async Task<string> GetAuto()
        {
            XDocument doc = XDocument.Load("https://media.cm.expert/stock/export/cmexpert/dealer.site/all/970f320baf6b7ca161e40e854dfc9629.xml");
            foreach (XElement elm in doc.Descendants("car"))
            {
                //Car info
                string CarId = "";
                string year = elm.Element("year").Value;
                string mark = elm.Element("mark_id").Value;
                string model = elm.Element("folder_id").Value;
                string VIN = elm.Element("vin").Value;
                string coast = elm.Element("price").Value;
                string eVoume = elm.Element("engine_volume").Value;
                string ePower = elm.Element("engine_power").Value;
                string gearbox = elm.Element("gearbox").Value;
                string drive = elm.Element("drive").Value;   
                string wheel = elm.Element("wheel").Value;
                DB db = new();
                db.openconn();
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT * FROM Машины WHERE VIN =@Vin", db.getconn());
                command.Parameters.AddWithValue("@Vin", VIN);
                
                adapter.SelectCommand = command;
                adapter.Fill(table);
                if (table.Rows.Count == 0)
                {
                    command = new("INSERT INTO Машины(Марка,Модель,VIN,Цена,Объем_двигателя,Мощность,Тип_автомобиля,Коробка,Привод,Тип_руля,Год) VALUES(@mark,@model,@VIN,@coast,@eVolume,@ePower,@type,@gearbox,@drive,@wheel,@year)", db.getconn());
                    command.Parameters.AddWithValue("@mark", mark);
                    command.Parameters.AddWithValue("@model", model);
                    command.Parameters.AddWithValue("@Vin", VIN);
                    command.Parameters.AddWithValue("@coast", coast);
                    command.Parameters.AddWithValue("@eVolume", eVoume);
                    command.Parameters.AddWithValue("@ePower", ePower);
                    command.Parameters.AddWithValue("@type", "Новый");
                    command.Parameters.AddWithValue("@gearbox", gearbox);
                    command.Parameters.AddWithValue("@drive", drive);
                    command.Parameters.AddWithValue("@wheel", wheel);
                    command.Parameters.AddWithValue("@year", year);
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "SELECT @@IDENTITY";
                    CarId = command.ExecuteScalar().ToString();



                    //Contact info

                    string contactId = "";
                    string phone = elm.Element("contact_info").Element("contact").Element("phone").Value;
                    command = new("INSERT INTO Информация_для_публикации(Контактный_телефон) VALUES(@phone)", db.getconn());
                    command.Parameters.AddWithValue("@phone", phone);
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "SELECT @@IDENTITY";
                    contactId = command.ExecuteScalar().ToString();
                    //Image
                    string idListImage = "";
                    command = new("INSERT INTO Список_изображений(Наименование) VALUES(@title)", db.getconn());
                    command.Parameters.AddWithValue("@title", mark + " " + model + " " + year);
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "SELECT @@IDENTITY";
                    idListImage = command.ExecuteScalar().ToString();

                    foreach (XElement imageEL in elm.Elements("images"))
                    {
                        
                        foreach (XElement element in imageEL.Elements("image"))
                        {
                            string image = element.Value;

                            command = new("INSERT INTO Изображения(Номер_списка,Изображение) VALUES(@list,@image)", db.getconn());
                            command.Parameters.AddWithValue("@list", idListImage);
                            command.Parameters.AddWithValue("@image", image);
                            await command.ExecuteNonQueryAsync();
                        }

                    }
                    
                   //Publish
                    string description = elm.Element("description").Value;
                    string Additional = elm.Element("extras").Value;

                    command = new("INSERT INTO Публикация(Номер_машины,Номер_информации,Список_изображений,Описание,Дополнительные_услуги) VALUES(@car,@info,@image,@descr,@add)", db.getconn());
                    command.Parameters.AddWithValue("@car", CarId);
                    command.Parameters.AddWithValue("@info", contactId);
                    command.Parameters.AddWithValue("@image", idListImage);
                    command.Parameters.AddWithValue("@descr", description);
                    command.Parameters.AddWithValue("@add", Additional);
                    await command.ExecuteNonQueryAsync();
                    db.closedconn();
                }
            }
            return "Ok";
        }
    }
}
