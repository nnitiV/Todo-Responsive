# 📝 Todo-Responsive

[![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-Entity%20Framework-512BD4?style=for-the-badge)](https://learn.microsoft.com/en-us/ef/core/)
[![Status](https://img.shields.io/badge/Status-Live-success?style=for-the-badge)](https://todo-app-responsive-in-c-net.onrender.com/)

A full-stack, multi-user Task Management application built with **ASP.NET Core Razor Pages**. 

This project goes beyond a simple "ToDo list" by implementing enterprise-level features such as **Role-Based Access Control (RBAC)**, **AJAX** for seamless updates, **File Uploads**, and **Dark Mode**.

🔗 **[Live Demo: View Website](https://todo-app-responsive-in-c-net.onrender.com/)**

---

### 🔑 Default Admin Login
The application seeds an Admin user on the first run. You can use these credentials to test the Admin Dashboard features:

* **Email:** `admin@gmail.com`
* **Password:** `Admin@123`

## ✨ Key Features

### 🔐 Security & Identity
* **Authentication:** Full Sign Up, Login, and Logout system using ASP.NET Core Identity.
* **Authorization:** Data isolation (Users can only see their own data).
* **Role-Based Access:** dedicated **Admin Dashboard** to manage all users and tasks.
* **Security:** Protection against IDOR (Insecure Direct Object References) and CSRF attacks.

### 🛠 Functionality
* **Task Management:** Create, Read, Update, and Delete (CRUD) tasks.
* **Categories:** Organize tasks into custom categories (Work, Personal, etc.) using One-to-Many relationships.
* **Profile Management:** Users can update their details and upload **Profile Pictures** (stored securely via file system).
* **AJAX Interactions:** Status updates and Deletions happen instantly without page reloads.

### 🎨 UI/UX
* **Responsive Design:** Fully mobile-friendly layout using **Bootstrap 5**.
* **Theming:** Persistent Light/Dark mode toggle (saved in LocalStorage).
* **Interactive UI:** Modals for confirmations and dynamic dropdown filters.

---

## 📸 Screenshots

| Landing Page | User Profile |
|:---:|:---:|
| ![InitialPage](https://github.com/user-attachments/assets/d1bbed11-079e-45ff-b3cd-17d1a02fa136) | ![UserProfileView](https://github.com/user-attachments/assets/a52590e6-e9d3-4cfa-bae5-5785d88cd9d9) |

| Task Management Demo | Admin Dashboard |
|:---:|:---:|
| ![TasksDemo](https://github.com/user-attachments/assets/c7d36f40-a75f-4b71-a177-4cbf71834f5c) | ![AdminDashboard](https://github.com/user-attachments/assets/08ba40f0-8d28-4f32-bbd0-ca1288c702ea) |

| Mobile / Dark Mode |
|:---:|
| ![DarkAndWhiteMode](https://github.com/user-attachments/assets/3bd6f59a-f412-4b7d-ba46-997ab04adb98) |
---

## 💻 Tech Stack

* **Framework:** ASP.NET Core 9.0 (Razor Pages)
* **Language:** C#
* **Database:** MySQL (Dev) / SQLite (Production)
* **ORM:** Entity Framework Core
* **Frontend:** HTML5, CSS3, Bootstrap 5.3, jQuery
* **Deployment:** Docker & Render

---

## 🚀 Getting Started

### Prerequisites
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
* MySQL Server (Optional - can switch to SQLite for local dev)

### Installation

1.  **Clone the repo**
    ```bash
    git clone [https://github.com/YOUR_USERNAME/Todo-Responsive.git](https://github.com/YOUR_USERNAME/Todo-Responsive.git)
    ```
2.  **Navigate to the project**
    ```bash
    cd Todo-Responsive/FirstWebApp
    ```
3.  **Restore dependencies**
    ```bash
    dotnet restore
    ```
4.  **Database Setup**
    The project is configured to auto-migrate on startup. Just ensure your connection string in `appsettings.json` is correct.
    ```bash
    dotnet ef database update
    ```
5.  **Run the app**
    ```bash
    dotnet run
    ```

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!
