# Job Application C#/.NET/Entity Framework Demo
## Description
The purpose of this .NET EF-CORE Application is to simulate a job application form to demonstrate EF-Core's abilities in creating a functional database to be used with C#. 
Once applications are entered into the database they can be listed and selected to view details of each record. This database involves a many to many 
relationship with the data inputed. Future versions of this database will see sorting features to help narrow down the best applicant for the job and will also track submission dates in the database.

The application should be fairly self explanatory as far as functionality but to naviagate the menu simply select an option by typing a Character within the '< >' & hit enter. Invalid entries will be discarded.

Menu options are:
| Enter Applicant | Remove Applicant | List Applicants | View Application | Quit |


## Featured Requirements Met:

### Create an additional class which inherits one or more properties from its parent
-Using EF Core to build the database context (DbContext) I was able to fulfill this requirement by creating the ApplicationContext Class which inherits from DbContext.

### Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
-One option of the demo is to view a list of all of the Applicant names. This list populates a text list that can then be used to retrieve data using the View Applicant option.

### Use a LINQ query to retrieve information from a data structure (such as a list or array) or file
-LINQ is used quite extensively in retrieving the data from each database table. A foreach loop within a foreach loop was implemented to retrieve all data before returning back to the menu.

### Build a conversion tool that converts user input to another type and displays it (ex: converts cups to grams)
-This requirement was met after as I collected the ComputerSkills information as a Boolean expression and then later converted all the booleans into a string for the user to retrieve the information.

### Calculate and display data based on an external factor 
-Today's Date is displayed when the program opens and when an application is submitted. In a later version of this app I intend to track the application submit date in the database. This feature also meets the requirement where one method must return a value to be used in the application.

### Analyze text and display information about it (ex: how many words in a paragraph)
-Whenenver a list of applicants is created a total application count is also included.
# Application_Demo
