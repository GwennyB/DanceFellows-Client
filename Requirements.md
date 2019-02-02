# VISION
West Coast Swing dance conventions are without a common system and toolset to manage event details, such as participants registration, competitor registration, and competition scoring/scorekeeping. This deficiency results in loss of floor time at most events, as well as loss of trust in Event Directors' competence when participants experience scheduling glitches.

Event Directors struggle to meet all those requirements as well as the expectations of participants - organization and tools are at the core of that struggle. The web apps for these events are largely ineffective (as a management tool), unsatisfying (in terms of user experience), and responsible for many schedule 'glitches' at events. A well-designed set of templates can address these issues.


# SCOPE

This project WILL...

    ...facilitate participant registration for Event Directors.
    ...facilitate and streamline competition planning and registration for Event Directors.
    ...rely on officially-sourced competitor data to support competition management.
    ...facilitate scorekeeping following a specific competition.

This project WILL NOT...

    ...serve as an e-commerce platform for merchandise.


# GOALS
## MVP for API:
    1. Model-based database with tables for competitors, series, events, results, and event comps.
    2. 3rd-party API reliance (for competitor data seeding).
    3. Routes to support competitor data and scoring.

## MVP for Web App:
    1. Full-stack ASP.NET Core MVC app.
    2. Sufficient functionality to support a single-event lifecycle with known competitors.
    3. User interfaces for:
        Event Director dashboard
        Competition creation
        Participants registration
        Raw score entry and retention

## Stretch Goals
    1. (API) Full scoring algorithm with event comp table update
    2. (Web App) View results during and after event
    3. (Web App) Functionality to add new competitors
    4. (Web App) View complete participants list


#FUNCTIONAL REQUIREMENTS
    1. Event Director can create and delete competitions.
    2. Event Director can create and delete partipants.
    3. Event Director can create competitor-competition associations.
    4. Event Director can enter raw scores for each competitor in each competition.
    5. Event Director can view competition rosters and raw scores once entered.

#NON-FUNCTIONAL REQUIREMENTS
    1. Testability
        - exclude Console.ReadLine() statements
        - ensure a retrievable return from every method
    2. Backup
        - ensure local copy of 3rd-party data in case API access is unavailable

#DATA FLOW
    Event Director lands on Dashboard. Available selections/routes:
        1. Create Competitions
            A form loads with select boxes for event type and level. On 'Add':
                A new row is added to the Competitions table (FrontDB).
                The new competition is reflected on the form, with 'delete' option available.
            On 'delete':
                The competition ID is used to query that row and delete it from Competitions table (FrontDB).
                The competitions list is updated to reflect the deletion.
        2. Register Participants
            A form loads with inputs for WSDC-ID, First Name, and Last Name. 
                If an ID is entered:
                    the ID is used to query the 'Competitors' table (APIDB), and results (if found) auto-populates first and last names.
                If no ID is entered:
                    user must enter first and last name to access 'Add' button.
                On 'Add', a new row is added to the 'Participants' table (FrontDB).
                Redirect to Register Participants to add more.
        3. Register competitors
            A form loads with select boxes populated from Participants and Competitions tables (FrontDB), and inputs for bib number and dancer role. On selections and Submit:
                An association is added to the Competition Registrations table, and role and bib number are added as payload.
            Redirect to Register Competitors to add more.
        4. Input scores
            A form loads showing all competitors and judges for a specific competition (from Competition Registratrations table query).
                User enters scores for all judge-competitor pairs. On Submit, Competition Registration table is updated with scores.
            Redirect to 'View Results' reflecting all scores.
        5. View Results
            Calls all Competition Registration records and displays them by competition and competitor.






