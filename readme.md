# Introduction

A sample project for custom authentication in a Blazor WASM app  
Note that this does NOT provide any security!
  
The sample simply checks if the user used `"Batman"` as a username and `"sponge"` as a password.  

## How it works

 - First you create a class that inherits from `AuthenticationStateProvider`
 - Then you register the custom implementation in `Program.cs`  
 - Finally,  you use the `<AuthorizeView>` component to show or hide content

