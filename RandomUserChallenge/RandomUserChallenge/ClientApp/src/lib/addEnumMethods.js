import _ from 'lodash';

export function nameOfEnumValue(enumObj) {
    const namesByValue = _.reduce(Object.keys(enumObj), (result, value) => {
        result[enumObj[value]] = _.capitalize(_.kebabCase(value).replace(/-/g, ' '));
        return result;
    }, {});

    return function (value) {
        if (value !== null) {
            return namesByValue.hasOwnProperty(value) ? namesByValue[value] : String(value);
        }

        return null;
    }
}

export function keyOfEnumValue(enumObj) {
    const namesByValue = _.reduce(Object.keys(enumObj), (result, value) => {
        result[enumObj[value]] = value;
        return result;
    }, {});

    return function (value) {
        if (value !== null) {
            return namesByValue.hasOwnProperty(value) ? namesByValue[value] : String(value);
        }

        return null;
    }
}

export function parseEnum(enumObj) {
    const propertyNamesByValue = _.reduce(Object.keys(enumObj), (result, name) => {
        if (!_.isFunction(name)) {
            result[String(enumObj[name])] = enumObj[name];
        }

        return result;
    }, {});

    return function (value) {
        if (value !== null) {
            return propertyNamesByValue.hasOwnProperty(String(value)) ? propertyNamesByValue[value] : enumObj[value] ;
        }

        return null;
    }
}

export default function addEnumMethods(enumObj) {
    enumObj.parse = parseEnum(enumObj);
    enumObj.nameOfEnumValue = nameOfEnumValue(enumObj);
    enumObj.keyOfEnumValue = keyOfEnumValue(enumObj);

    return enumObj;
}