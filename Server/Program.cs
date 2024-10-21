using Radzen;
using EspoNew.Server.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024).AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "EspoNewTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<EspoNew.Server.EspoDbNewService>();
builder.Services.AddDbContext<EspoNew.Server.Data.EspoDbNewContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EspoDbNewConnection"));
});
builder.Services.AddControllers().AddOData(opt =>
{
    var oDataBuilderEspoDbNew = new ODataConventionModelBuilder();
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.AccountsAccount>("Accountsaccounts");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>("Accountsaccount_contacts");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>("Accountsaccount_documents");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>("Addressaddress_cities");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>("Addressaddress_countries");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.AddressAddressState>("Addressaddress_states");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.CallsCall>("Callscalls");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.CallsCallContact>("Callscall_contacts");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.CallsCallLead>("Callscall_leads");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>("Campaigncampaigns");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.CasesCase>("Cases_cases");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>("Casescase_contacts");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.ContactsContact>("Contactscontacts");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>("Contactscontact_documents");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>("Contactscontact_meetings");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>("Contactscontact_opportunities");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>("Documentsdocuments");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>("Documentsdocument_leads");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.EmailEmail>("Emailemails");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>("Emailemail_accounts");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>("Employeesemployees");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.LeadsLead>("Leadsleads");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>("Meetingsmeetings");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>("Opportunitiesopportunities");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.TargetTarget>("Targettargets");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.TargetTargetList>("Targettarget_lists");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.TasksTask>("Taskstasks");
    oDataBuilderEspoDbNew.EntitySet<EspoNew.Server.Models.EspoDbNew.TeamsTeam>("Teamsteams");
    opt.AddRouteComponents("odata/EspoDbNew", oDataBuilderEspoDbNew.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<EspoNew.Client.EspoDbNewService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AddInteractiveWebAssemblyRenderMode().AddAdditionalAssemblies(typeof(EspoNew.Client._Imports).Assembly);
app.Run();