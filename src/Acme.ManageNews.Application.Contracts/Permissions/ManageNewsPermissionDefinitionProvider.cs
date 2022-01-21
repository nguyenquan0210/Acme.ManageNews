using Acme.ManageNews.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.ManageNews.Permissions;

public class ManageNewsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ManageNewsPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ManageNewsPermissions.MyPermission1, L("Permission:MyPermission1"));

        var categoriesPermission = myGroup.AddPermission(ManageNewsPermissions.Categories.Default, L("Permission:Categories"));
        categoriesPermission.AddChild(ManageNewsPermissions.Categories.Create, L("Permission:Categories.Create"));
        categoriesPermission.AddChild(ManageNewsPermissions.Categories.Edit, L("Permission:Categories.Edit"));
        categoriesPermission.AddChild(ManageNewsPermissions.Categories.Delete, L("Permission:Categories.Delete"));

        var citiesPermission = myGroup.AddPermission(ManageNewsPermissions.Cities.Default, L("Permission:Cities"));
        citiesPermission.AddChild(ManageNewsPermissions.Cities.Create, L("Permission:Cities.Create"));
        citiesPermission.AddChild(ManageNewsPermissions.Cities.Edit, L("Permission:Cities.Edit"));
        citiesPermission.AddChild(ManageNewsPermissions.Cities.Delete, L("Permission:Cities.Delete"));

        var topicsPermission = myGroup.AddPermission(ManageNewsPermissions.Topics.Default, L("Permission:Topics"));
        topicsPermission.AddChild(ManageNewsPermissions.Topics.Create, L("Permission:Topics.Create"));
        topicsPermission.AddChild(ManageNewsPermissions.Topics.Edit, L("Permission:Topics.Edit"));
        topicsPermission.AddChild(ManageNewsPermissions.Topics.Delete, L("Permission:Topics.Delete"));

        var eventsPermission = myGroup.AddPermission(ManageNewsPermissions.Events.Default, L("Permission:Events"));
        eventsPermission.AddChild(ManageNewsPermissions.Events.Create, L("Permission:Events.Create"));
        eventsPermission.AddChild(ManageNewsPermissions.Events.Edit, L("Permission:Events.Edit"));
        eventsPermission.AddChild(ManageNewsPermissions.Events.Delete, L("Permission:Events.Delete"));

        var newsPermission = myGroup.AddPermission(ManageNewsPermissions.News.Default, L("Permission:News"));
        newsPermission.AddChild(ManageNewsPermissions.News.Create, L("Permission:News.Create"));
        newsPermission.AddChild(ManageNewsPermissions.News.Edit, L("Permission:News.Edit"));
        newsPermission.AddChild(ManageNewsPermissions.News.Delete, L("Permission:News.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ManageNewsResource>(name);
    }
}
