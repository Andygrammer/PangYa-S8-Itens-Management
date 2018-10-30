# PangYa-S8-Itens-Management
App to handle items of a PangYa S8 private server (by Acrisio) in a friendly way.

# 0. Version History
v0.1 - Card vol.1 Manager </br>
v0.11 - Card vol.2 Manager (under development) </br>
v0.12 - Card vol.3 Manager (under development) </br>
v0.13 - Card vol.4 Manager (under development) </br>
v0.2 - Club Manager(coming soon)

# 1. Project Solution Structure
a) Main class: Program.cs </br>
b) Model folder: contain the entities classes (User, Card, Club...) </br>
c) DAO folder: contain the classes those manipulate the entities using a database server </br>
d) Resources folder: contain the default images used by the app </br>
e) SQL folder: contain sub-folders Procedures (for created procedures) and Scripts (for created sql scripts) </br>
f) Forms: the application windows

# 2. How to run the solution
a) Update the resources file (Resources.resx) with your PangYa S8 server information. Obs.: You don't need to update the proc resources </br></br>
b) Update the config file (Settings.settings) </br>
b.1) To get a valid "userId" and "userNickname" values, execute the following command in your database: "SELECT UID, NICK FROM pangya.account;"  </br></br>
c) You need to create 1 table and 5 procedures in your PangYa S8 server database. In order to do that, execute the following files in MySQL Workbench or in your RDBMS: </br>
c.1) Create Table z_imgt_card_images.sql </br>
c.2) Z_IMGT_GetAllCards.sql </br>
c.3) Z_IMGT_GetAllUsers.sql </br>
c.4) Z_IMGT_GetCardQuantity.sql </br>
c.5) Z_IMGT_GetUserInfo.sql </br>
c.6) Z_IMGT_SetCardQuantity.sql </br></br>
d) Confirm there is a reference called "MySQL.Data" in references of project (Visual Studio) </br></br>
e) Confirm there is the following default tables in your database (those tables are used by the Procedures of this solution </br>
e.1) pangya_cards </br>
e.2) account </br>
e.3) pangya_card </br></br>
f) Review the previous steps

# 3. Images of application running
a) See "img" folder

# 4. Credits
a) This application was developed by André Marinho </br></br>
b) The PangYa S8 Private Server was developed by Acrisio
