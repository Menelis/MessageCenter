# Message Center Windows Service
On your requirements I was not sure if you wanted a Windows Service or API but I opted to use .NET 5 Windows service called Worker Service.

### Product Structure
- Core
    - This is a layer where all business rules are applied
- Infastructure
    - This layer is telling our service where we are saving or receiving data from.
    - For this project we only need two tables:
        - RunningYear - This table will store years on which birthday messages are sent. There is no need to populate this table. The service will populate it with each current year when it runs
        - BirthDaySentLogs - This table marks the birthday wishes that has been sent for each employee in a specific year.
    - Change the appsettings.json ConnectionString to reflect your server details
    - The migrations has been created already under Migrations folder
    - Perform this command on Infastructure project "directory dotnet ef database update -c MessageCenterDbContext"
- MessageCenterServer
    - This is a Windows service that can every time of the day depending on the delay set based on requirements.
    - This service get Employees from Realm Digital Empolyee API
    - How to run the application:
        - Clone the repository
        - Build whole solution
        - Update the following values on appsettings.json(for production deployment) or appsettings.Development.json(for running locally)
            - ApplicationSettings
                - Email - This is the email where birthday wishes will be sent if the Employee API does not have property called Email.
            - EmailSettings - These are email configuration for sending email.
                - From - The Sender
                - SmtpServer - The mail server(you can change it if you not using gmail)
                - Port - Mail Server Port(you can change it if you not using email)
                - Username - The username for mail server(if the mail server you are using does not require authentication you can leave this empty)
                - Password - The password for mail server(if the mail server you are using does not require authentication you can leave this empty).
                

# HAPPY CODING!!!!!!!!!!!!!