# GradeChaser
## Climbing grade conversion web api
### C# / ASP.NET

Converts Font, Yosemite Decimal System and V-Scale grades to one another (approximately). Uses the conversions rates from https://climbinghouse.com/grades-charts/.

The API has one POST endpoint at /api/convert that expects the following object in its request body.

```
{
  "from": 0,
  "to": 0,
  "rating": "string"
}
```

"from" and "to" expect a value 0-2 (0 - Font, 1 - YDS, 2 - V-Scale), where rating is the string in the grade style of "from".

7A+ Font grade conversion to V-Scale

Example request body:

```
{
  "from": 0,
  "to": 2,
  "rating": "7A+"
}
```

Example result:

```
{
  "system": 2,
  "grade": "V7"
}
```
