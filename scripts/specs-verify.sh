#!/bin/bash

EXIT_CODE=0

PARAMS="{ \
    \"apiUrl\": \"http://localhost:5002/haalcentraal/api\", \
    \"logFileToAssert\": \"./test-data/logs/reisdocumenten-informatie-service.json\", \
    \"oAuth\": { \
        \"enable\": false \
    } \
}"

npx cucumber-js -f json:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie.json \
                -f summary:./test-reports/cucumber-js/step-definitions/test-result-zonder-dependency-integratie-summary.txt \
                -f summary \
                features/docs \
                -p UnitTest \
                > /dev/null
if [[ $? -ne 0 ]]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/reisdocumenten/test-result.json \
                -f summary:./test-reports/cucumber-js/reisdocumenten/test-result-reisdocumentnummer-summary.txt \
                -f summary \
                features/raadpleeg-met-reisdocumentnummer \
                -p InfoApi \
                > /dev/null
if [[ $? -ne 0 ]]; then EXIT_CODE=1; fi

npx cucumber-js -f json:./test-reports/cucumber-js/reisdocumenten/test-result.json \
                -f summary:./test-reports/cucumber-js/reisdocumenten/test-result-burgerservicenummer-summary.txt \
                -f summary \
                features/zoek-met-burgerservicenummer \
                -p InfoApi \
                > /dev/null
if [[ $? -ne 0 ]]; then EXIT_CODE=1; fi

# Exit with error code if any command failed
exit $EXIT_CODE