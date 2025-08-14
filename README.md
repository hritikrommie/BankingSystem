**Banking System**

still updating readme....

**How to run** :\
1.Download the code and build it, and then run it.\
2.Please import the collection, it is there in the repository.\
3.In postman, all variables are set, so no need to change any request.\
4.For furhter test or to test any particular behaviour of api, please change request properties.\

Postman Collection : https://github.com/hritikrommie/BankingSystem/blob/master/BankingSystem.postman_collection.json \

## 📜 Features

- **User Management**
  - Register, Login, GetUsers.
  - Password hashing & secure authentication using JWT.
- **Account Operations**
  - View account details.
  - Perform deposit, withdrawal, and transfer operations.
- **Security**
  - JWT Authentication & role-based authorization.
  - Request rate limiting.
- **Performance**
  - Response caching for frequently accessed endpoints.
- **Architecture**
  - Uses **Repository + Unit of Work** pattern.
  - **AutoMapper** for DTO ↔ Entity mapping.
- **Testing**
  - Unit tests with mocked Unit of Work & Logger.


