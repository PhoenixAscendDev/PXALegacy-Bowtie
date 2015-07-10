# Bowtie Readme

Bowtie is currently being developed using Repository Design Pattern principles, Kanban as the agile framework of choice and Azure as the back-office cloud based solution.


This project currently includes the following

* 1 Visual Studio solution
* 8 Visual Studio projects.
* 1 Asset folder 

##Project Websites

* API Service - http://jb2-bowtie-api.azurewebsites.net/
* Product - http://bowtie.jbsquared.com/ , http://jb2-bowtie.azurewebsites.net/ 

##Project Databases
* JB2-Bowtie - Production 
 * cx8yl1ofvr.database.windows.net (will mirgrate to new server closer to launch)
* JB2-Dev - Development
* cx8yl1ofvr.database.windows.net

project's database prefix:  "jb2bt" 
all Jbsquared project database objects are prefix to simplify data resources and scaleability between servers and databases. 


## VS Projects
### Client API
* **Bowtie.ClientREST**
 * API dll file for Web and REST enabled applications
 * *Namespace:  JB2.Bowtie.Client.Web*

### Class Library
* **Bowtie.Lib**
 * Main Class Libraby that is referenced in all projects
 *  *Namespace:  JB2.Bowtie*
* **Bowtie.Service**
 * Service layer for business rules and logic
 * *Namespace:  JB2.Bowtie.Service*
* **Bowtie.RepoLinq**
 * Data Access layer connecting to Bowtie SQL database
 * *Namespace:  JB2.Bowtie.Data*
* **Bowtie.RepoNoDB**
 * Data Access layer used for unit testing and simplied data
 * *Namespace:  JB2.Bowtie.Data.NoDB*

### Web Application
* **Bowtie.WebAPI**
 * Web API that applications communicate with to send or recieve data
 * *Namespace:  JB2.Bowtie.WebAPI*
* **Bowtie.WebMain**
 * Standard website that contains both the brochure site and client game configuration functionality
 * *Namespace: JB2.Bowtie.Web*

### Testing
* **Bowtie.ConsoleTest**
 * TEST project for Console (windows exe) based applications
 * *Namespace: Bowtie.ConsoleTest*
* **Bowtie.WebTest**
 * Placeholder for quick test
 * *Namespace: Bowtie.WebTest*
