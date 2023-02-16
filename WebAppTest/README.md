
# Test design report

## Input variables for New Order page
- First name - string
- Last name - string 
- Email - string
- Birth date - pick date
- From - select from list
- To - from list
- Flight date - pick date
- Flight id - radio box from list
- Coupon - string
- Discount - select from list

## Input variable transformation 
To be able to check for boundary values, we do a following transformation of input data.
### Birth date -> age
- If Birth date not None, then:
	- **Age** = Current date - Birth date
- Otherwise:
	- **Age** = None

## Boundary values and equivalence classes
We transformed the input variables to equivalence classes in the following way.

### First name / Last name 
| Class    | Values in class                   | Example value |
| ------- | --------------------------------- | ------------- |
| None    | null                              | null          |
| Valid   | length > 0, only A-Z or a-z       | Pepa          |
| Invalid | length > 0, other characters used | Pep007        |

###  Email 
| Class    | Values in class          | Example value        |
| ------- | ------------------------ | -------------------- |
| None    | null                     | null                 |
| Valid   | length > 0, email format | mullemi5@fel.cvut.cz |
| Invalid | length > 0, other format | pepa.cz              |

### Age
- For reading simplification, we use years instead of datetime here.
- We identified following boundary values:
	- None - input not filled
	- 15 - minimum age to be able to book a flight
	- 26 - age limit for student discount
	- 65 - minimum age for senior discount
	- 150 - age limit to be able to book a flight

| Class    | Values in class | Low value | Mid Value | High value |
| -------- | --------------- | --------- | --------- | ---------- |
| None     | null            |           | null      |            |
| TooYoung | <-inf, 15)      | -inf      | 3         | 14.9       |
| Student  | <15, 26)        | 15        | 18        | 25.9       |
| Adult    | <26, 65)        | 26        | 35        | 64.9       |
| Senior   | <65, 150)       | 65        | 70        | 149.9      |
| TooOld   | <150, inf)      | 150       | 200       |            |

### From / To
These variables are selected from a list of valid values for flight search and thus always valid on their own - no need to parametrize. 

| Class | Values in class | Example from | Example to |
| ----- | --------------- | ------------ | ---------- |
| Valid | city from list  | Prague       | Krakow     |

### Flight Date
Flight date is used only for flight search, which might or might not give results depending on stored flights (we do not pose restrictions on it, except only January 2023 flights can be selected in this version of the app).

| Class | Values in class | Example value |
| ----- | --------------- | ------------- |
| None  | null            | null          | 
| Valid | date |   2023-01-01       |

### Flight id
It depends whether a flight is selected or not.
| Class    | Values in class | Example value                          |
| -------- | --------------- | -------------------------------------- |
| None     | null            | null                                   |
| Selected | valid id        | "caed89cf-b332-4634-95e0-7eddb8606170" |

### Coupon
| Class    | Values in class                     | Example value |
| ------- | ----------------------------------- | ------------- |
| None    | null                                | null          |
| Valid   | length > 0, in accepted coupon list | CHRISTMAS10   |
| Invalid | length > 0, not in list             | random007     |

### Discount
All options for discount must be checked.
| Class   | Values in class          | Example value        |
| ------- | ------------------------ | -------------------- |
| None    | null                     | null                 |
| Student  | Student | Student |
| Senior | Senior | Senior             |

## Input Data Combinations
We used the ACTS tool to generate two .csv files with pairwise combined inputs. The first one includes succsesfull combinations of input data, the second one includes such combinations, that at leat one constraint was violated. 

## Process testing
We created a UML activity diagram for the software - https://drive.google.com/file/d/1txzlj3zjFkQZOlAsa_Anc-IfDAW93CDp/view?usp=sharing . Nevertheless such graph has unimportant decision points. When simplified, it results in a pretty flat graph with two main parts. It was therefore not necessary to create path-based tests for our case and we created the tests manually, based on processes from the business point of view - eg. a user can submit a flight and then see it in the orders.