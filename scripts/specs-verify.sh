#!/bin/bash

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
                --tags "not @integratie" \
                --tags "not @skip-verify"

npx cucumber-js -f json:./test-reports/cucumber-js/reisdocumenten/test-result.json \
                -f summary:./test-reports/cucumber-js/reisdocumenten/test-result-reisdocumentnummer-summary.txt \
                -f summary \
                features/raadpleeg-met-reisdocumentnummer \
                --tags "not @skip-verify" \
                --world-parameters "$PARAMS"

npx cucumber-js -f json:./test-reports/cucumber-js/reisdocumenten/test-result.json \
                -f summary:./test-reports/cucumber-js/reisdocumenten/test-result-burgerservicenummer-summary.txt \
                -f summary \
                features/zoek-met-burgerservicenummer \
                --tags "not @skip-verify" \
                --world-parameters "$PARAMS"
