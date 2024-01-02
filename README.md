# Personal Assistant Application - Report

# Introduction:
The Personal Assistant application is designed to facilitate a teacher's tasks both inside and outside the classroom by managing students' attendance, grade information, and in-class participation. The primary goal of the application is to streamline the teacher's responsibilities and save time.

## In-App Operations:

##  Login:
Access to the application is restricted to registered teachers. Users can log in with their usernames and passwords.

##  Menu:
The menu consists of three panels. The first panel displays information about the logged-in user, including Name, Surname, and e-mail. The second panel contains date and time information. The third panel serves as the operations panel, where users can perform key tasks within the application: Classroom, Grades, Reminder, Calculator, and Logout.

##  Classroom:

Selecting a class is the initial step in this operation. After clicking the List button, the registered students for the selected class are displayed in the list view.
Actions can be performed on these students, such as marking the absence of a student with the "Not Here" button, which increments the attendance record in the database.
Another action involves rewarding a participating student with extra points. Clicking the button increases the plusPoint data in the database, allowing the teacher to manually add this extra point to the final exam later.
##  Grades:

Similar to the Classroom operation, the user starts by selecting a class. The DataGridView displays the students of the chosen class.
Changes can be made to the database on columns related to Midterm 1, Midterm 2, and Final exams.
The page also calculates the final grade if all three exam scores are entered. If a student has more than four absences in the Classroom, the student is marked as FAIL, and the letter grade is not calculated.
##  Reminder:

The Reminder operation allows the teacher to keep notes on tasks to be done both inside and outside the classroom. These notes are stored in the database and remain accessible.
##  Calculator:

A basic calculator is included for the teacher's use as needed during class.
##7. Logout:

Initiates the return to the login screen.
Conclusion:
The Personal Assistant application is a comprehensive tool for teachers, enabling efficient management of attendance, grades, reminders, and basic calculations. Its user-friendly interface is designed to simplify tasks and save time, contributing to an enhanced teaching experience.
