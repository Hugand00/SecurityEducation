# Digital Security Education

## Description

This project is a digital education program aimed at individuals with limited knowledge of cybersecurity, information security, data protection, and AI. The goal is to provide fundamental understanding and awareness of these critical topics.

The education is organized into three chapters:

1. **Cybersecurity**
2. **Information Security and Data Protection**
3. **Artificial Intelligence (AI)**

Each chapter contains 3-5 educational sections.

This project was developed as a Bachelor’s thesis (level B) by two students in Informatics with a specialization in Systems Development during the Spring term of 2025.

---

## Technical Information

* Platform: ASP.NET MVC
* Programming Languages: C#, JavaScript, CSS, HTML
* Learning Standard: xAPI (Experience API) implemented for tracking user activity
* Learning Record Store: Veracity
* Database: PostgreSQL (The database is not provided; however, its structure can be created from the code using terminal commands)

---

## Installation and Running Instructions

1. Clone or download the repository.
2. Open the project in Visual Studio (2019 or later recommended).
3. Make sure PostgreSQL is installed and configured on your machine or server.
4. Create the database structure by running the provided terminal commands/scripts from the codebase.
5. Update the connection string in `appsettings.json` or the relevant configuration file to point to your PostgreSQL database.
6. Setup LRS with Veracity and update access keys and user information.
7. Build and run the project in Visual Studio.

---

## Structure

* **Chapters**: Three main chapters, each with multiple sections.
* **Content**: Includes text, images, and interactive components displayed in the browser.
* **Tracking**: User progress and activity are logged using xAPI.

---

## Dependencies

* .NET Framework&#x20;
* PostgreSQL
* xAPI components and Veracity

---

## Contributors

* [Hugand00](https://github.com/Hugand00)
* [pettersoderblom](https://github.com/pettersoderblom)

---

## 📜 License

This project is free for personal and educational use. Contact the authors for permission before using it for commercial purposes.
