using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Bussiness.HouseRepository;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using Data.Model;
namespace Bussiness.Crawler
{
    public class SeleniumServices : ISeleniumServices
    {
        private readonly IHouseRepository _houseRepository;

        public SeleniumServices(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }

        private const string extractsThePropertiesLinkJavascript = @"var hrefsList = [];
                          var elemetList = document.getElementById('list_cart_holder').querySelectorAll('.main_items');
                          for (index = 0; index < elemetList.length; index++)
                          {
                             hrefsList.push(elemetList[index].href)
                          }
                          return hrefsList; ";

        private const string propertyAttributesJavascript = @"var porpDict = {};
                    function getChildNodes(node) {
                            var children = new Array();
                            for(var child in node.childNodes) {
                                if(node.childNodes[child].nodeType == 1 && node.childNodes[child].className != 'checkboxes_space') {
                                    children.push(node.childNodes[child]);
                                }
                            }
                            return children;
                        }
                    propList = getChildNodes(document.getElementById('extra-fields'));
                    for (index=0; index<propList.length;index++){
                            porpDict[propList[index].children[0].textContent.trim()] = propList[index].children[1].textContent.trim();
                    }
                    porpDict['Pret']=document.getElementById('price').textContent.trim().split(' ')[0].replace('.','');
                            return porpDict;";

        private List<string> ExtractsThePropertiesLink(string pageNumberLink, PhantomJSDriver driver)
        {
            var propertiesLink = new List<string>();
                driver.Navigate().GoToUrl(pageNumberLink);
                try
                {
                    IJavaScriptExecutor js = driver;
                    var linkList = (ReadOnlyCollection<object>) js.ExecuteScript(extractsThePropertiesLinkJavascript);
                    return linkList.ToArray().ToList().Select(s => (string) s).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            return null;
        }

        public string ExtractDataFromWeb()
        {
            var page = 1;
            var housesSite = "https://homezz.ro/anunturi_de-vanzare_in-iasi-is.html";
            var options = new PhantomJSOptions();
            options.AddAdditionalCapability("IsJavaScriptEnabled", true);
            using (var driver = new PhantomJSDriver(@"C:\Users\cosmin\.nuget\packages\phantomjs\2.1.1\tools\phantomjs",
                options))
            {
                while (page < 3)
                {
                    var linkList = ExtractsThePropertiesLink(housesSite,driver);
                    foreach (var link in linkList)
                    {
                        driver.Navigate().GoToUrl((string) link);
                        try
                        {
                            IJavaScriptExecutor js = driver;
                            Dictionary<string,object> propertyDictionary = (Dictionary<string,object>) js.ExecuteScript(propertyAttributesJavascript);
                            var house = DictionaryToHouseObject(propertyDictionary);
                            _houseRepository.Add(house);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    page++;
                    housesSite = "https://homezz.ro/anunturi_de-vanzare_in-iasi-is_" + page + ".html";
                }
            }
            return null;
        }

        private House DictionaryToHouseObject(Dictionary<string, object> propertyDictionary)
        {
            var house  = new House();
            foreach (var item in propertyDictionary)
            {
                switch (item.Key)
                {
                    case "Zona":
                        house.Neighborhood = item.Value.ToString();
                        break;
                    case "Tip imobil":
                        house.PropertyType = item.Value.ToString();
                        break;
                    case "Suprafața utilă":
                        string surface = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.LivingSurface = Int32.Parse(surface);
                        break;
                    case "Stare proprietate":
                        house.PropertyStatus = item.Value.ToString();
                        break;
                    case "Posibilitate parcare":
                        bool canParking;
                        if (item.Value.ToString() == "Da")
                            canParking = true;
                        else
                            canParking = false;
                        house.PossibilityOfParking = canParking;
                        break;
                    case "Număr niveluri imobil":
                        string floorNumber = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.TotalNumberOfFloors = Int32.Parse(floorNumber);
                        break;
                    case "Număr camere":
                        string numberOfRomms = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.NumberOfRooms = Int32.Parse(numberOfRomms);
                        break;
                    case "Număr Băi":
                        string numberOfBath = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.NumberOfBathrooms = Int32.Parse(numberOfBath);
                        break;
                    case "Nr. locuri parcare":
                        string numberOfParkingSpaces = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.NumberOfParkingSpaces = Int32.Parse(numberOfParkingSpaces);
                        break;
                    case "Mobilat/Utilat":
                        house.FurnishedAndFit = item.Value.ToString();
                        break;
                    case "Etaj":
                        string floor = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.Floor = Int32.Parse(floor);
                        break;
                    case "Confort":
                        house.Comfort = item.Value.ToString();
                        break;
                    case "Compartimentare":
                        house.Partitioning = item.Value.ToString();
                        break;
                    case "An finalizare construcție":
                        string yearBuilt = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.YearBuilt = Int32.Parse(yearBuilt);
                        break;
                    case "Pret":
                        string price = new String(item.Value.ToString().Where(Char.IsDigit).ToArray());
                        house.Price = Int32.Parse(price);
                        break;
                    
                }
            }
            return house;
        }
    }
}