The purpose of this project is to show the clean architecture of how a .NET project should be built.

The project is cut into 4 projects, 1 API and 3 Class Library projects (separation of concerns) and following the generic repository pattern.

The domain is so dummy, it contains the interfaces, models, and DTOs.

The Infrastructure handles the connectivity with the DB so it contains the context, the migrations and the implementation of the generic repository.

The Service is the implementation of the interfaces of the services written in the Domain layer. The Service layer is where the business logic of the app resides.

The WebApi project is also dummy it doesn't contain any logic or interactions with the database, it only does the calls of the CRUD operations of the services. It also contains the middlewares, example: Exceptions middleware.
