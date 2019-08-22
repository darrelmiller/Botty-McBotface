# Botty McBotface
<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/ExpertsInside/Botty-McBotface">
    <img src="https://avilox.de/avilox_NEU/wp-content/uploads/2018/07/ExpertsInside.jpg" height="80px" alt="Logo">
  </a>

  <h2 align="center">Botty McBotface</h2>

  <p align="center">
    An example on how to interacte with Graph API from ASP.NET Core
    <br />
    <a href="https://github.com/expertsinside/Botty-McBotface"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/expertsinside/Botty-McBotface/issues">Report Bug</a>
    <a href="https://github.com/expertsinside/Botty-McBotface/issues">Request Feature</a>
  </p>
</p>

<!-- TABLE OF CONTENTS -->
## Table of Contents

- [Botty McBotface](#botty-mcbotface)
  - [Table of Contents](#table-of-contents)
  - [About The Project](#about-the-project)
    - [Built With](#built-with)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
  - [Roadmap](#roadmap)
  - [Contributing](#contributing)
  - [License](#license)
  - [Contact](#contact)
  - [Acknowledgements](#acknowledgements)

<!-- ABOUT THE PROJECT -->
## About The Project

[![Botty McBotface][product-screenshot]][demo-url]

This is demo project that I use for live session or live on Twitch when I explain how the Microsoft Graph API works and how can we integrate them with ASP.NET Core.
After some experiments this project became a real product.
I pushed the source code on Github for this lite version. This is a kind of starter kit you can use to start your project and add the features you need.
It's very simple and below you can find some useful information that helps you to understand the flow of the Azure AD authentication and how to retrieve a token to call the Microsoft Graph API.

### Built With

This project is written with Visual Studio Code, ASP.NET Core, C#, Microsoft Bot Framework and Graph API.

- [ASP.NET Core](https://asp.net)
- [Graph API](https://graph.microsoft.com)
- [Bot Framework](https://dev.botframework.com)

<!-- GETTING STARTED -->
## Getting Started

Download or fork the project from this repository and open it in Visual Studio 2019 or Visual Studio Code.
Restore all packages from Nuget and press F5 to start Debug.
Launch the Bot Framework emulator and insert the correct endpoint for your Bot.

![bot emulator](botemulator.png)

### Prerequisites

By default this project uses an application registered on our Azure AD tenant. You can use that for your test, but if you want to use this project for your own product, you have to create a new application on your tenant.
Follow these steps:

- Login to portal.azure.com
- Enter in the section "Azure Active Directory"
- Click on "App Registrations"
- Create a new application
- In the "API permissions" section, click on "Add permission" and select Graph API
- Select the permission "Group.ReadWrite.All, Directory.ReadWrite.All"
- Grant the permissions in the previous screen
- From the main section "App Registrations", select "Authentication" and enable the checkbox "ID Tokens"
- Copy all entries in the "appsettings.json" from the project and paste them in the "Redirect URIs" section

### Installation

You can publish the application on Azure or on your favorite cloud platform.
The requirements are:

- HTTPS
- .NET Core 2.2 Runtime

<!-- ROADMAP -->
## Roadmap

See the [open issues](https://github.com/expertsinside/botty-mcbotface/issues) for a list of proposed features (and known issues).

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT -->
## Contact

Emanuele Bartolesi - [@kasuken](https://twitter.com/kasuken) - eba@expertsinside.com

Project Link: [https://github.com/expertsinside/botty-mcbotface](https://github.com/expertsinside/botty-mcbotface)

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements

- [ASP.NET Core](https://www.asp.net)
- [Graph API](https://graph.microsoft.com)
- [Bot Framework](https://dev.botframework.com)
- [Choose an Open Source License](https://choosealicense.com)

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/expertsinside/botty-mcbotface.svg?style=flat-square
[contributors-url]: https://github.com/expertsinside/botty-mcbotface/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/expertsinside/botty-mcbotface.svg?style=flat-square
[forks-url]: https://github.com/expertsinside/botty-mcbotface/network/members
[stars-shield]: https://img.shields.io/github/stars/expertsinside/botty-mcbotface.svg?style=flat-square
[stars-url]: https://github.com/expertsinside/botty-mcbotface/stargazers
[issues-shield]: https://img.shields.io/github/issues/expertsinside/botty-mcbotface.svg?style=flat-square
[issues-url]: https://github.com/expertsinside/botty-mcbotface/issues
[license-shield]: https://img.shields.io/github/license/expertsinside/botty-mcbotface.svg?style=flat-square
[license-url]: https://github.com/expertsinside/botty-mcbotface/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/expertsinside
[product-screenshot]: bottydemo.gif
