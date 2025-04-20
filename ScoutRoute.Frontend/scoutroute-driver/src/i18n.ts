import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import daJSON from "./locale/da.json";
import enJSON from "./locale/en.json";

i18n.use(initReactI18next).init({
    resources: {
        en: { ...enJSON },
        da: { ...daJSON },
    },
    lng: "da",     // Set the initial language of the App
});