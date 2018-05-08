This page describes how we track issues in the `vsts-outsystems` repository.

## Popular queries

- [Inbox tracking](https://github.com/OutSystems/vsts/issues?utf8=%E2%9C%93&q=is%3Aopen%20no%3Aassignee%20-label%3Afeature-request%20-label%3Atestplan-item%20-label%3Aplan-item%20-label%3Aextension-candidate)
- [Verification Needed](https://github.com/OutSystems/vsts/issues?q=is%3Aissue+-label%3Averified+is%3Aclosed+label%3Averification-needed)

## Inbox tracking and Issue triage
New issues or pull requests submitted by the community are triaged by a team member. The team rotates the inbox tracker on a weekly basis.

### Inbox Tracking

The [Inbox query](https://github.com/OutSystems/vsts/issues?utf8=%E2%9C%93&q=is%3Aopen%20no%3Aassignee%20-label%3Afeature-request%20-label%3Atestplan-item%20-label%3Aplan-item%20-label%3Aextension-candidate) contains all the
- **open issues or pull requests** that
- are **not feature requests** nor **test plan items** nor **plan items** nor **extension candidates** and
- have **no owner assignment**.

The **inbox tracker** should do the following initial triage:
- Is the issue **invalid**? Close it and justify the reason.
- Is the issue **a general question**, like *How can I compile TypeScript*? Close it and redirect the user to [Stack Overflow](http://stackoverflow.com/questions/tagged/vsts-outsystems) with [this message](https://gist.github.com/joalmeid/c26148d5d20cd542a6a2899abf6fa37a).

**Everyone** should do the following secondary triage to their assigned issues (the **inbox tracker** may do some of these steps too, if obvious):
- If an issue needs more info, assign the `needs more info` and ask for more information in a comment.
- Ensure that the issue has a **type** label, that is, `bug`, `feature-request`, `debt`, `needs more info`
- Do a best effort to identify duplicates
- If the issue is a feature-request then the initial owner optionally unassigns himself from the issue. 
- If the issue is an important `bug`, assign an `important` label and optionally mention @OutSystems/vsts to get the attention of the entire team.
- If the issue needs to be fixed in this release, assign it to the current milestone (eg: blocks a scenario, completes a new feature, etc.). 
- If needed, follow-up with the author.

## Verification

Issues need to be verified.

Verification is a service that you request from others either implicitly with the `bug`-label or explicitly with the `verification-needed`-label. Find issue that are to be verified with these queries

- [bugs to be verified](https://github.com/OutSystems/vsts/issues?utf8=%E2%9C%93&q=is%3Aissue%20label%3Abug%20-label%3Averified%20is%3Aclosed%20-label%3Aduplicate%20-label%3Ainvalid%20)
- [verification needed](https://github.com/OutSystems/vsts/issues?q=is%3Aissue+-label%3Averified+is%3Aclosed+label%3Averification-needed)

Follow the these rules:

1. Query for issues that are to be verified
2. Start with issues you created (filter by `Author`) but didn't close
3. Pick an item
  - Start with setting `verified`-label (prevents duplicate verifications)
  - Verify the issue
  - If the issue still shows, add the `verification-found`-label and remove the `verified`-label
  - Go back to #3


## Duplicates
Duplicate bugs are closed with a comment `duplicates #issue`. Please try to reference an earlier issue **unless** a later issue is more appropriate (has more context, better scenarios, repro steps, etc.).

