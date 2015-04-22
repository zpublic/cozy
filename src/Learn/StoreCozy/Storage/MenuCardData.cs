using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using StoreCozy.Model;

namespace StoreCozy.Storage
{
    [DataContract(Name = "MenuCard")]
    public class MenuCardData
    {
        public MenuCardData() 
        { 
        
        }

        public MenuCardData(MenuCard menuCard)
        {
            this.Title = menuCard.Title;
            this.Description = menuCard.Description;
            this.ImagePath = menuCard.ImagePath;
            MenuItems = new List<MenuItemData>();
            foreach (var item in menuCard.MenuItems)
            {
                MenuItems.Add(new MenuItemData(item));
            }
        }

        public MenuCard ToMenuCard()
        {
            var menuCard = new MenuCard
            {
                Title = this.Title,
                Description = this.Description,
                ImagePath = this.ImagePath
            };
            foreach (MenuItemData item in this.MenuItems)
            {
                menuCard.MenuItems.Add(item.ToMenuItem());
            }
            menuCard.ClearDirty();
            return menuCard;
        }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public List<MenuItemData> MenuItems { get; set; }
    }
}
