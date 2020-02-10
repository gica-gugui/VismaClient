# Visma client assignment

Known issues:

- No unit tests (unfortunately I couldn't manage my time better to fully complete the requirements)
- Client not fully tested as I couldn't find any credentials for a testing environment
- Requests will fail with a forbidden response status code, because no credentials are provided.

The application architecture is based on .NET Core Middleware. The Middleware consists of pipelines, different for enterprise and partner interfaces.
I have created two delegating handlers in HttpClient. These handlers are configured to act like interceptors, when performing a request. Depending on the interface we want to access (enterprise or partner), the handlers perform the following operations:

1. For the enterprise interface, the handler computes and sets the authorization header.
2. For the partner inferace, the handler requests a bearer token and sets the authorization header using the bearer token.

I have created services to GET and POST documents and to GET an invitation using the enterprise interface, and to GET an organization using the partner interface.

Regarding serializing and deserializing objects, I have added a naming policy which converts the property names to snake cases (e.g. property_name). The policy is set in the object performing the serialization and deserialization.

The application uses configuration files, and supports multiple environments (Staging, Production).
