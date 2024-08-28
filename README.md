# ShfMgmtEg

A management system project with a clear separation of concerns, where the API handles backend logic and the MVC project serves as the frontend interface.

## Project Structure

- **ShfMgmtEg.Api**: A RESTful API providing endpoints for various operations.
- **ShfMgmtEg.Mvc**: A frontend project that interacts with the API to render the user interface.

## Features

- **API Layer**: Built with `ShfMgmtEg.Api`, offering comprehensive backend services.
- **MVC Frontend**: Developed in `ShfMgmtEg.Mvc`, this project consumes the API endpoints to render the user interface.

## Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/moradchalaby/ShfMgmtEg.git
   ```
2. **Navigate to the Project Directory**:

```bash
cd ShfMgmtEg
```
3. **Restore NuGet Packages**:

```bash
dotnet restore
```

4. **Build the Solution**:
```bash
dotnet build
```

5. **Run the API**:
```bash
dotnet run --project ShfMgmtEg.Api
```
6. **Run the MVC Application**:
```bash
dotnet run --project ShfMgmtEg.Mvc
```

## Usage
- **Access the Web Application**: After running the MVC project, visit `http://localhost:5000` in your browser.
- **API Endpoints**: The MVC project communicates with the `ShfMgmtEg.Api` for all backend operations.

## Contributing
Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add your feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a Pull Request.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

```vbnet
This README provides an organized overview of the project structure, installation steps, and usage 
```

