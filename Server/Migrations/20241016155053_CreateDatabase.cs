using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EspoNew.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "Address");

            migrationBuilder.EnsureSchema(
                name: "Calls");

            migrationBuilder.EnsureSchema(
                name: "Campaign");

            migrationBuilder.EnsureSchema(
                name: "Cases");

            migrationBuilder.EnsureSchema(
                name: "Contacts");

            migrationBuilder.EnsureSchema(
                name: "Documents");

            migrationBuilder.EnsureSchema(
                name: "Email");

            migrationBuilder.EnsureSchema(
                name: "Employees");

            migrationBuilder.EnsureSchema(
                name: "Leads");

            migrationBuilder.EnsureSchema(
                name: "Meetings");

            migrationBuilder.EnsureSchema(
                name: "Opportunities");

            migrationBuilder.EnsureSchema(
                name: "Target");

            migrationBuilder.EnsureSchema(
                name: "Tasks");

            migrationBuilder.EnsureSchema(
                name: "Teams");

            migrationBuilder.CreateTable(
                name: "address_country",
                schema: "Address",
                columns: table => new
                {
                    country_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    country_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_country", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "campaign",
                schema: "Campaign",
                columns: table => new
                {
                    campaign_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Planning')"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Email')"),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    budget = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    mail_merge_only_with_address = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))"),
                    budget_currency = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    contacts_template_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    leads_template_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    accounts_template_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    users_template_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaign", x => x.campaign_id);
                });

            migrationBuilder.CreateTable(
                name: "email_account",
                schema: "Email",
                columns: table => new
                {
                    email_account_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Active')"),
                    host = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    port = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((993))"),
                    security = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'SSL')"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    monitored_folders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sent_folder = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    store_sent_emails = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    keep_fetched_emails_unread = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    fetch_since = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    fetch_data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    connected_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    use_imap = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))"),
                    use_smtp = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    smtp_host = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    smtp_port = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((587))"),
                    smtp_auth = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))"),
                    smtp_security = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'TLS')"),
                    smtp_username = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    smtp_password = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    smtp_auth_mechanism = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'login')"),
                    imap_handler = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    smtp_handler = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    email_folder_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_account", x => x.email_account_id);
                });

            migrationBuilder.CreateTable(
                name: "target",
                schema: "Target",
                columns: table => new
                {
                    target_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    salutation_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'')"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'')"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_street = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_city = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_state = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_country = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_postal_code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    do_not_call = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_target", x => x.target_id);
                });

            migrationBuilder.CreateTable(
                name: "target_list",
                schema: "Target",
                columns: table => new
                {
                    target_list_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    assigned_user_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_target_list", x => x.target_list_id);
                });

            migrationBuilder.CreateTable(
                name: "team",
                schema: "Teams",
                columns: table => new
                {
                    team_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    position_list = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    layout_set_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    working_time_calendar_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.team_id);
                });

            migrationBuilder.CreateTable(
                name: "address_state",
                schema: "Address",
                columns: table => new
                {
                    state_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    state_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_state", x => x.state_id);
                    table.ForeignKey(
                        name: "FK_address_state_address_country_country_id",
                        column: x => x.country_id,
                        principalSchema: "Address",
                        principalTable: "address_country",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "lead",
                schema: "Leads",
                columns: table => new
                {
                    lead_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    salutation_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'New')"),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    industry = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    opportunity_amount = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_street = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_city = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_state = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_country = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_postal_code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    do_not_call = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    converted_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    account_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    opportunity_amount_currency = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    campaign_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    created_account_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    created_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    created_opportunity_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lead", x => x.lead_id);
                    table.ForeignKey(
                        name: "FK_lead_campaign_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "Campaign",
                        principalTable: "campaign",
                        principalColumn: "campaign_id");
                });

            migrationBuilder.CreateTable(
                name: "address_city",
                schema: "Address",
                columns: table => new
                {
                    city_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    city_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_city", x => x.city_id);
                    table.ForeignKey(
                        name: "FK_address_city_address_state_state_id",
                        column: x => x.state_id,
                        principalSchema: "Address",
                        principalTable: "address_state",
                        principalColumn: "state_id");
                });

            migrationBuilder.CreateTable(
                name: "account",
                schema: "Accounts",
                columns: table => new
                {
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    website = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    industry = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    sic_code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    billing_address_street = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    billing_address_city = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    billing_address_state = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    billing_address_country = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    billing_address_postal_code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    shipping_address_street = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    shipping_address_city = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    shipping_address_state = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    shipping_address_country = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    shipping_address_postal_code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    campaign_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_account_campaign_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "Campaign",
                        principalTable: "campaign",
                        principalColumn: "campaign_id");
                });

            migrationBuilder.CreateTable(
                name: "call",
                schema: "Calls",
                columns: table => new
                {
                    call_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Planned')"),
                    date_start = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_end = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    direction = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Outbound')"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    parent_type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_call", x => x.call_id);
                    table.ForeignKey(
                        name: "FK_call_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "contact",
                schema: "Contacts",
                columns: table => new
                {
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    salutation_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    do_not_call = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    address_street = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_city_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    address_state_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    address_country_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    address_postal_code = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    campaign_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_contact_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_contact_address_city_address_city_id",
                        column: x => x.address_city_id,
                        principalSchema: "Address",
                        principalTable: "address_city",
                        principalColumn: "city_id");
                    table.ForeignKey(
                        name: "FK_contact_address_country_address_country_id",
                        column: x => x.address_country_id,
                        principalSchema: "Address",
                        principalTable: "address_country",
                        principalColumn: "country_id");
                    table.ForeignKey(
                        name: "FK_contact_address_state_address_state_id",
                        column: x => x.address_state_id,
                        principalSchema: "Address",
                        principalTable: "address_state",
                        principalColumn: "state_id");
                    table.ForeignKey(
                        name: "FK_contact_campaign_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "Campaign",
                        principalTable: "campaign",
                        principalColumn: "campaign_id");
                });

            migrationBuilder.CreateTable(
                name: "email",
                schema: "Email",
                columns: table => new
                {
                    email_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    from_string = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    reply_to_string = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    address_name_map = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_replied = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    message_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    message_id_internal = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    body_plain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_html = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Archived')"),
                    has_attachment = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    date_sent = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    delivery_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    is_system = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    ics_contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ics_event_uid = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    from_email_address_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    parent_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    parent_type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    sent_by_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    replied_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    created_event_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    created_event_type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    group_folder_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email", x => x.email_id);
                    table.ForeignKey(
                        name: "FK_email_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "meeting",
                schema: "Meetings",
                columns: table => new
                {
                    meeting_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Planned')"),
                    date_start = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_end = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    is_all_day = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((0))"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_start_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_end_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    parent_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    parent_type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meeting", x => x.meeting_id);
                    table.ForeignKey(
                        name: "FK_meeting_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                });

            migrationBuilder.CreateTable(
                name: "call_lead",
                schema: "Calls",
                columns: table => new
                {
                    call_lead_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    call_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    lead_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'None')"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_call_lead", x => x.call_lead_id);
                    table.ForeignKey(
                        name: "FK_call_lead_call_call_id",
                        column: x => x.call_id,
                        principalSchema: "Calls",
                        principalTable: "call",
                        principalColumn: "call_id");
                    table.ForeignKey(
                        name: "FK_call_lead_lead_lead_id",
                        column: x => x.lead_id,
                        principalSchema: "Leads",
                        principalTable: "lead",
                        principalColumn: "lead_id");
                });

            migrationBuilder.CreateTable(
                name: "account_contact",
                schema: "Accounts",
                columns: table => new
                {
                    account_contact_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    is_inactive = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_contact", x => x.account_contact_id);
                    table.ForeignKey(
                        name: "FK_account_contact_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_account_contact_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                });

            migrationBuilder.CreateTable(
                name: "call_contact",
                schema: "Calls",
                columns: table => new
                {
                    call_contact_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    call_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'None')"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_call_contact", x => x.call_contact_id);
                    table.ForeignKey(
                        name: "FK_call_contact_call_call_id",
                        column: x => x.call_id,
                        principalSchema: "Calls",
                        principalTable: "call",
                        principalColumn: "call_id");
                    table.ForeignKey(
                        name: "FK_call_contact_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                });

            migrationBuilder.CreateTable(
                name: "case",
                schema: "Cases",
                columns: table => new
                {
                    case_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'New')"),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Normal')"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    lead_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    inbound_email_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case", x => x.case_id);
                    table.ForeignKey(
                        name: "FK_case_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_case_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                    table.ForeignKey(
                        name: "FK_case_lead_lead_id",
                        column: x => x.lead_id,
                        principalSchema: "Leads",
                        principalTable: "lead",
                        principalColumn: "lead_id");
                });

            migrationBuilder.CreateTable(
                name: "employee",
                schema: "Employees",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    employee_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'regular')"),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    auth_method = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    api_key = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    salutation_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    is_active = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((1))"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    avatar_color = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    delete_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'0')"),
                    middle_name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    default_team_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    avatar_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    dashboard_template_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    working_time_calendar_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    layout_set_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_employee_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                });

            migrationBuilder.CreateTable(
                name: "contact_meeting",
                schema: "Contacts",
                columns: table => new
                {
                    contact_meeting_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    meeting_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'None')"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_meeting", x => x.contact_meeting_id);
                    table.ForeignKey(
                        name: "FK_contact_meeting_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                    table.ForeignKey(
                        name: "FK_contact_meeting_meeting_meeting_id",
                        column: x => x.meeting_id,
                        principalSchema: "Meetings",
                        principalTable: "meeting",
                        principalColumn: "meeting_id");
                });

            migrationBuilder.CreateTable(
                name: "case_contact",
                schema: "Cases",
                columns: table => new
                {
                    case_contact_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    case_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_case_contact", x => x.case_contact_id);
                    table.ForeignKey(
                        name: "FK_case_contact_case_case_id",
                        column: x => x.case_id,
                        principalSchema: "Cases",
                        principalTable: "case",
                        principalColumn: "case_id");
                    table.ForeignKey(
                        name: "FK_case_contact_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                });

            migrationBuilder.CreateTable(
                name: "document",
                schema: "Documents",
                columns: table => new
                {
                    document_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Active')"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    publish_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    expiration_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    folder_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_document_employee_assigned_employee_id",
                        column: x => x.assigned_employee_id,
                        principalSchema: "Employees",
                        principalTable: "employee",
                        principalColumn: "employee_id");
                });

            migrationBuilder.CreateTable(
                name: "opportunity",
                schema: "Opportunities",
                columns: table => new
                {
                    opportunity_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    amount = table.Column<double>(type: "float", nullable: true, defaultValueSql: "(NULL)"),
                    stage = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Prospecting')"),
                    last_stage = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    probability = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    lead_source = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    close_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount_currency = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    campaign_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_opportunity", x => x.opportunity_id);
                    table.ForeignKey(
                        name: "FK_opportunity_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_opportunity_campaign_campaign_id",
                        column: x => x.campaign_id,
                        principalSchema: "Campaign",
                        principalTable: "campaign",
                        principalColumn: "campaign_id");
                    table.ForeignKey(
                        name: "FK_opportunity_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                    table.ForeignKey(
                        name: "FK_opportunity_employee_assigned_employee_id",
                        column: x => x.assigned_employee_id,
                        principalSchema: "Employees",
                        principalTable: "employee",
                        principalColumn: "employee_id");
                });

            migrationBuilder.CreateTable(
                name: "task",
                schema: "Tasks",
                columns: table => new
                {
                    task_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Not Started')"),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'Normal')"),
                    date_start = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_end = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_start_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_end_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    date_completed = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(NULL)"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parent_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    parent_type = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    assigned_employee_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    email_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_task_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_task_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                    table.ForeignKey(
                        name: "FK_task_email_email_id",
                        column: x => x.email_id,
                        principalSchema: "Email",
                        principalTable: "email",
                        principalColumn: "email_id");
                    table.ForeignKey(
                        name: "FK_task_employee_assigned_employee_id",
                        column: x => x.assigned_employee_id,
                        principalSchema: "Employees",
                        principalTable: "employee",
                        principalColumn: "employee_id");
                });

            migrationBuilder.CreateTable(
                name: "account_document",
                schema: "Accounts",
                columns: table => new
                {
                    account_document_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    account_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    document_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_document", x => x.account_document_id);
                    table.ForeignKey(
                        name: "FK_account_document_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "Accounts",
                        principalTable: "account",
                        principalColumn: "account_id");
                    table.ForeignKey(
                        name: "FK_account_document_document_document_id",
                        column: x => x.document_id,
                        principalSchema: "Documents",
                        principalTable: "document",
                        principalColumn: "document_id");
                });

            migrationBuilder.CreateTable(
                name: "contact_document",
                schema: "Contacts",
                columns: table => new
                {
                    contact_document_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    document_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_document", x => x.contact_document_id);
                    table.ForeignKey(
                        name: "FK_contact_document_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                    table.ForeignKey(
                        name: "FK_contact_document_document_document_id",
                        column: x => x.document_id,
                        principalSchema: "Documents",
                        principalTable: "document",
                        principalColumn: "document_id");
                });

            migrationBuilder.CreateTable(
                name: "document_lead",
                schema: "Documents",
                columns: table => new
                {
                    document_lead_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    document_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    lead_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_lead", x => x.document_lead_id);
                    table.ForeignKey(
                        name: "FK_document_lead_document_document_id",
                        column: x => x.document_id,
                        principalSchema: "Documents",
                        principalTable: "document",
                        principalColumn: "document_id");
                    table.ForeignKey(
                        name: "FK_document_lead_lead_lead_id",
                        column: x => x.lead_id,
                        principalSchema: "Leads",
                        principalTable: "lead",
                        principalColumn: "lead_id");
                });

            migrationBuilder.CreateTable(
                name: "contact_opportunity",
                schema: "Contacts",
                columns: table => new
                {
                    contact_opportunity_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    contact_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    opportunity_id = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "(NULL)"),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(NULL)"),
                    deleted = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_opportunity", x => x.contact_opportunity_id);
                    table.ForeignKey(
                        name: "FK_contact_opportunity_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "Contacts",
                        principalTable: "contact",
                        principalColumn: "contact_id");
                    table.ForeignKey(
                        name: "FK_contact_opportunity_opportunity_opportunity_id",
                        column: x => x.opportunity_id,
                        principalSchema: "Opportunities",
                        principalTable: "opportunity",
                        principalColumn: "opportunity_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_assigned_employee_id",
                schema: "Accounts",
                table: "account",
                column: "assigned_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_campaign_id",
                schema: "Accounts",
                table: "account",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_contact_account_id",
                schema: "Accounts",
                table: "account_contact",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_contact_contact_id",
                schema: "Accounts",
                table: "account_contact",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_document_account_id",
                schema: "Accounts",
                table: "account_document",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_document_document_id",
                schema: "Accounts",
                table: "account_document",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_city_state_id",
                schema: "Address",
                table: "address_city",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_address_state_country_id",
                schema: "Address",
                table: "address_state",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_call_account_id",
                schema: "Calls",
                table: "call",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_call_contact_call_id",
                schema: "Calls",
                table: "call_contact",
                column: "call_id");

            migrationBuilder.CreateIndex(
                name: "IX_call_contact_contact_id",
                schema: "Calls",
                table: "call_contact",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_call_lead_call_id",
                schema: "Calls",
                table: "call_lead",
                column: "call_id");

            migrationBuilder.CreateIndex(
                name: "IX_call_lead_lead_id",
                schema: "Calls",
                table: "call_lead",
                column: "lead_id");

            migrationBuilder.CreateIndex(
                name: "IX_case_account_id",
                schema: "Cases",
                table: "case",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_case_contact_id",
                schema: "Cases",
                table: "case",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_case_lead_id",
                schema: "Cases",
                table: "case",
                column: "lead_id");

            migrationBuilder.CreateIndex(
                name: "IX_case_contact_case_id",
                schema: "Cases",
                table: "case_contact",
                column: "case_id");

            migrationBuilder.CreateIndex(
                name: "IX_case_contact_contact_id",
                schema: "Cases",
                table: "case_contact",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_account_id",
                schema: "Contacts",
                table: "contact",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_address_city_id",
                schema: "Contacts",
                table: "contact",
                column: "address_city_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_address_country_id",
                schema: "Contacts",
                table: "contact",
                column: "address_country_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_address_state_id",
                schema: "Contacts",
                table: "contact",
                column: "address_state_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_campaign_id",
                schema: "Contacts",
                table: "contact",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_document_contact_id",
                schema: "Contacts",
                table: "contact_document",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_document_document_id",
                schema: "Contacts",
                table: "contact_document",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_meeting_contact_id",
                schema: "Contacts",
                table: "contact_meeting",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_meeting_meeting_id",
                schema: "Contacts",
                table: "contact_meeting",
                column: "meeting_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_opportunity_contact_id",
                schema: "Contacts",
                table: "contact_opportunity",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_opportunity_opportunity_id",
                schema: "Contacts",
                table: "contact_opportunity",
                column: "opportunity_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_assigned_employee_id",
                schema: "Documents",
                table: "document",
                column: "assigned_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_lead_document_id",
                schema: "Documents",
                table: "document_lead",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_document_lead_lead_id",
                schema: "Documents",
                table: "document_lead",
                column: "lead_id");

            migrationBuilder.CreateIndex(
                name: "IX_email_account_id",
                schema: "Email",
                table: "email",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_contact_id",
                schema: "Employees",
                table: "employee",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_lead_campaign_id",
                schema: "Leads",
                table: "lead",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_meeting_account_id",
                schema: "Meetings",
                table: "meeting",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_opportunity_account_id",
                schema: "Opportunities",
                table: "opportunity",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_opportunity_assigned_employee_id",
                schema: "Opportunities",
                table: "opportunity",
                column: "assigned_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_opportunity_campaign_id",
                schema: "Opportunities",
                table: "opportunity",
                column: "campaign_id");

            migrationBuilder.CreateIndex(
                name: "IX_opportunity_contact_id",
                schema: "Opportunities",
                table: "opportunity",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_account_id",
                schema: "Tasks",
                table: "task",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_assigned_employee_id",
                schema: "Tasks",
                table: "task",
                column: "assigned_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_contact_id",
                schema: "Tasks",
                table: "task",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_email_id",
                schema: "Tasks",
                table: "task",
                column: "email_id");

            migrationBuilder.AddForeignKey(
                name: "FK_account_employee_assigned_employee_id",
                schema: "Accounts",
                table: "account",
                column: "assigned_employee_id",
                principalSchema: "Employees",
                principalTable: "employee",
                principalColumn: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_campaign_campaign_id",
                schema: "Accounts",
                table: "account");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_campaign_campaign_id",
                schema: "Contacts",
                table: "contact");

            migrationBuilder.DropForeignKey(
                name: "FK_account_employee_assigned_employee_id",
                schema: "Accounts",
                table: "account");

            migrationBuilder.DropTable(
                name: "account_contact",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "account_document",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "call_contact",
                schema: "Calls");

            migrationBuilder.DropTable(
                name: "call_lead",
                schema: "Calls");

            migrationBuilder.DropTable(
                name: "case_contact",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "contact_document",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "contact_meeting",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "contact_opportunity",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "document_lead",
                schema: "Documents");

            migrationBuilder.DropTable(
                name: "email_account",
                schema: "Email");

            migrationBuilder.DropTable(
                name: "target",
                schema: "Target");

            migrationBuilder.DropTable(
                name: "target_list",
                schema: "Target");

            migrationBuilder.DropTable(
                name: "task",
                schema: "Tasks");

            migrationBuilder.DropTable(
                name: "team",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "call",
                schema: "Calls");

            migrationBuilder.DropTable(
                name: "case",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "meeting",
                schema: "Meetings");

            migrationBuilder.DropTable(
                name: "opportunity",
                schema: "Opportunities");

            migrationBuilder.DropTable(
                name: "document",
                schema: "Documents");

            migrationBuilder.DropTable(
                name: "email",
                schema: "Email");

            migrationBuilder.DropTable(
                name: "lead",
                schema: "Leads");

            migrationBuilder.DropTable(
                name: "campaign",
                schema: "Campaign");

            migrationBuilder.DropTable(
                name: "employee",
                schema: "Employees");

            migrationBuilder.DropTable(
                name: "contact",
                schema: "Contacts");

            migrationBuilder.DropTable(
                name: "account",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "address_city",
                schema: "Address");

            migrationBuilder.DropTable(
                name: "address_state",
                schema: "Address");

            migrationBuilder.DropTable(
                name: "address_country",
                schema: "Address");
        }
    }
}
