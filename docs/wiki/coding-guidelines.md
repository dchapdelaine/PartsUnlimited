
#Coding Guidelines

## Git

We prefer a **feature branches** for development team. For contributors we encourage to fork the repo and make pull requests. 

## Indentation
We use tabs, not spaces.

## Names
* Use PascalCase for `type` names
* Use PascalCase for `enum` values
* Use camelCase for `function` and `method` names
* Use camelCase for `property` names and `local variables`
* Use whole words in names when possible

## Types
* Do not export `types` or `functions` unless you need to share it across multiple components
* Do not introduce new `types` or `values` to the global namespace

## Comments
* Use [JSDoc](http://usejsdoc.org/about-getting-started.html) style comments for `functions`, `interfaces`, `enums`, and `classes`

## Strings
* Use "double quotes" for strings shown to the user that need to be externalized (localized)
* Use 'single quotes' otherwise 
* All strings visible to the user need to be externalized

## Style
* Use arrow functions `=>` over anonymous function expressions
* Always surround loop and conditional bodies with curly braces
* Open curly braces always go on the same line as whatever necessitates them
