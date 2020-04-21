import AppActions from './../actions/AppActions';

const newLine = `
`;

export function bindCheckedTo(statePath) {
    return (event) =>  AppActions.updateState(statePath, event.target.checked);
}

export function bindMultiLineToArray(statePath) {
    const getValue = (event) => event.target.value.split(newLine);

    return (event) =>  AppActions.updateState(statePath, getValue(event));
}

export default function bindValueTo(statePath) {
    return (event) =>  AppActions.updateState(statePath, event.target.value);
}