using System.Threading.Tasks;
using Acme.ManageNews.Localization;
using Acme.ManageNews.MultiTenancy;
using Acme.ManageNews.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Acme.ManageNews.Web.Menus;

public class ManageNewsMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<ManageNewsResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                ManageNewsMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        var bookStoreMenu = new ApplicationMenuItem(
            "ManageNews",
            l["Menu:Categories"],
            icon: "fa fa-book"
        );
        context.Menu.AddItem(bookStoreMenu);

        //CHECK the PERMISSION
        if (await context.IsGrantedAsync(ManageNewsPermissions.Categories.Default))
        {
            bookStoreMenu.AddItem(new ApplicationMenuItem(
                "ManageNews.Category",
                l["Menu:Categories"],
                url: "/Categories"
            ));
        }
        if (await context.IsGrantedAsync(ManageNewsPermissions.Cities.Default))
        {
            bookStoreMenu.AddItem(new ApplicationMenuItem(
                "ManageNews.City",
                l["Menu:Cities"],
                url: "/Cities"
            ));
        }

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
    }
}
