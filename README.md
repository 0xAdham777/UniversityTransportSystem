# University Transport System

## Overview

**University Transport System** is a comprehensive Windows desktop application designed to manage and optimize university transportation services. The system centralizes the management of students, employees, drivers, transport infrastructure, subscriptions, schedules, and daily transportation operations within a single integrated platform.

The application supports the complete transportation workflow, from defining geographic areas and transport lines to tracking student attendance, managing subscriptions and payments, assigning buses and drivers, and recording operational incidents.

Built using a modern **3-tier architecture**, the project emphasizes scalability, maintainability, and separation of concerns through dedicated Presentation, Business, and Data Access layers.

---

# Key Features

## Academic Management

* Manage university departments and specialties
* Associate students with academic structures
* Organize educational entities efficiently

## People Management

* Student registration and tracking
* Employee management
* Driver management
* Shared person profile system

## Geographic Management

* Wilaya management
* Municipality management
* Transport station management
* Geographic hierarchy organization

## Transport Infrastructure

* Bus model management
* Bus fleet management
* Transport line configuration
* Line-station mapping
* Schedule management

## Daily Transport Operations

* Trip management
* Bus assignment management
* Student trip attendance tracking
* Transport subscription handling
* Subscription payment processing
* Incident and incident type management

---

# System Architecture

The project follows a **classic 3-tier architecture**:

## 1. Presentation Layer (WPF Desktop Application)

Responsible for:

* User interface
* User interactions
* Navigation and forms
* Data presentation

Technologies:

* WPF
* XAML
* C#
* Dependency Injection

Structure:

* Views organized by domain:

  * Academic
  * Geography
  * People
  * Transport
  * Operations

---

## 2. Business Layer

Responsible for:

* Business logic
* Service orchestration
* Validation rules
* Domain models
* Application services

Features:

* Generic service architecture
* Service contracts/interfaces
* Reusable business components
* Strong separation from UI and database

---

## 3. Data Access Layer

Responsible for:

* SQL Server communication
* CRUD operations
* Repository pattern implementation
* Database configuration and connection management

Features:

* Generic repository base class
* ADO.NET implementation
* SQL Server optimization
* Centralized database helpers

---

# Technologies Used

| Technology                               | Purpose                               |
| ---------------------------------------- | ------------------------------------- |
| C# / .NET 8                              | Core application language and runtime |
| WPF                                      | Desktop application framework         |
| XAML                                     | UI layout and styling                 |
| Microsoft.Data.SqlClient 5.2             | SQL Server connectivity               |
| ADO.NET                                  | Database access technology            |
| SQL Server                               | Relational database system            |
| Microsoft.Extensions.DependencyInjection | Dependency injection container        |
| Visual Studio 2022                       | Development environment               |
| MVVM-like Architecture                   | Separation of concerns                |

---

# Database Design

The backend database (**UniversityTransportDB**) includes:

* 21 relational tables
* Foreign key constraints
* Indexed relationships
* Seed data support

Main entities include:

* Students
* Drivers
* Employees
* Buses
* Bus Models
* Transport Lines
* Stations
* Trips
* Subscriptions
* Payments
* Attendance
* Incidents

---

# Design Patterns & Practices

The project incorporates several software engineering best practices:

## Repository Pattern

Encapsulates database operations and improves maintainability.

## Dependency Injection

Provides loose coupling between components.

## Generic Services & Repositories

Reduces code duplication and increases reusability.

## Layered Architecture

Ensures clean separation between:

* UI
* Business Logic
* Data Access

## MVVM-inspired Structure

Improves organization of WPF views and logic.

---

# Project Structure

The solution is divided into three primary projects:

## UniversityTransportProject

Presentation layer containing:

* WPF views
* Forms
* Navigation
* UI resources

## UniversityTransportProject.Business

Business layer containing:

* Models
* Services
* Interfaces
* Domain logic

## UniversityTransportProject.DataAccess

Data layer containing:

* Repositories
* Database infrastructure
* SQL communication utilities

---

# Technical Highlights

* Fully modular architecture
* Strong domain separation
* Scalable repository system
* Reusable CRUD infrastructure
* Centralized SQL Server management
* Large-scale desktop business application structure
* Professional enterprise-style organization

---

# Educational Value

This project demonstrates practical implementation of:

* Enterprise desktop development
* Layered software architecture
* WPF application design
* SQL Server integration
* Repository & Service patterns
* Dependency Injection in .NET
* Real-world transportation management workflows

It serves as an excellent example of a medium-to-large scale academic management and transport operations system built with modern .NET technologies.
