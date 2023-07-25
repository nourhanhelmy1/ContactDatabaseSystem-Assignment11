# Contact Database Web Application

## Overview
The Contact Database Web Application is a simple ASP.NET Core Razor Pages project that serves as a basic contact management system. It allows users to login with their credentials and access specific views based on their roles. The application provides two different views, one for users and another for administrators. User passwords are securely encrypted using the ASP.NET Core Identity PasswordHasher. Users can also log out from the application.

## Features

1. **Login:**
   - Users can access the login page (index.cshtml) to sign in with their username and password.
   - If a user enters an incorrect username or password, the application displays an indicative error message, either "Invalid Username" or "Incorrect Password".

2. **Different Views for Each Role:**
   - The application has two separate views for different user roles: User.cshtml and Admin.cshtml.
   - Users with the "User" role are directed to the User.cshtml page after login.
   - Administrators with the "Admin" role are directed to the Admin.cshtml page after login.

3. **Password Encryption:**
   - User passwords are securely encrypted using the ASP.NET Core Identity PasswordHasher.
   - Password hashing provides an additional layer of security to protect sensitive user information.

4. **Logout Function:**
   - Both the User.cshtml and Admin.cshtml pages contain a logout button.
   - Clicking the logout button logs the user out from the application.
   - Users are redirected back to the login page (index.cshtml) after successful logout.

5. **Contact Form:**
   - Only administrators can add new contacts to the database using the contact form.
   - The contact form includes the following fields:
     - First Name (required)
     - Last Name (required)
     - Email (required)
     - Title (Mr, Mrs, etc) (required)
     - Description (additional information about the contact)
     - Date of Birth (date picker)
     - Marriage Status (checkbox for indicating whether the contact is married)
     - Role (Administrator or User)
     - Username (can only consist of letters and numbers)
     - Password (is atleast 8 characters)

6. **Contact List:**
   - All logged-in users, both users, and administrators can access a list of all contacts.
   - The contact list displays the following details for each contact:
     - First Name
     - Last Name
     - Email
     - Title
     - Description
     - Date of Birth
     - Marriage Status

7. **Search Filter:**
   - The contact list includes a search bar that allows users and administrators to filter contacts based on the first name, last name, or email.
   - The search filter helps users quickly find specific contacts within the list.


## Application Structure

- **Pages Folder:**
  - **Index.cshtml:** The login page where users enter their credentials.
  - **User.cshtml:** The user-specific view accessible after successful login.
  - **Admin.cshtml:** The admin-specific view accessible after successful login.
  - **Shared Folder:** Contains the _Layout.cshtml file used as the common layout for all pages.

## Getting Started

1. **Prerequisites:**
   - .NET 5.0 SDK or later versions installed on your machine.
   - EdgeDB database and appropriate connection string configured.

2. **Clone the Repository:**
   - Clone this repository to your local machine.

3. **Database Setup:**
   - Configure the EdgeDB connection.

4. **Run the Application:**
   - Open a terminal or command prompt in the project directory and run the following command:
     ```
     dotnet run
     ```
   - The application should now be running on `http://localhost:5000`.

5. **Accessing the Application:**
   - Open your web browser and navigate to `http://localhost:5000`.
   - You will be redirected to the login page (index.cshtml).
   - Enter your credentials and click the "Login" button.
   - If the credentials are valid, you will be directed to the appropriate user or admin view.

## Contributing

Contributions to the Contact Database Web Application are welcome! If you find any bugs or want to suggest enhancements, feel free to open an issue or create a pull request.

## Here are some Screenshots:

<img width="960" alt="Screenshot 2023-07-25 045850" src="https://github.com/nourhanhelmy1/ContactDatabaseSystem-Assignment11/assets/117583711/d0513533-d466-4548-a351-266563e1aa5c">

<img width="960" alt="Screenshot 2023-07-25 045939" src="https://github.com/nourhanhelmy1/ContactDatabaseSystem-Assignment11/assets/117583711/21d31eb7-c805-438d-b5d2-ec7041695aec">

<img width="960" alt="Screenshot 2023-07-25 045957" src="https://github.com/nourhanhelmy1/ContactDatabaseSystem-Assignment11/assets/117583711/e720cd7f-46ae-4c26-bf11-8bd589019b0c">

<img width="960" alt="Screenshot 2023-07-25 050045" src="https://github.com/nourhanhelmy1/ContactDatabaseSystem-Assignment11/assets/117583711/c9966c11-7d9e-4d42-9a4e-2eebe91f73a3">


<img width="960" alt="image" src="https://github.com/nourhanhelmy1/ContactDatabaseSystem-Assignment11/assets/117583711/484528ba-8f83-4bfe-ba53-58d51f189331">

