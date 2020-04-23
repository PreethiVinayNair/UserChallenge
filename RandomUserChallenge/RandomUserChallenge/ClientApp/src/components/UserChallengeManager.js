import React, { Component } from 'react';
import AppActions from './../actions/AppActions';
import bindValueTo, { bindCheckedTo, bindMultiLineToArray } from '../lib/bindValueTo';
import selectn from 'selectn';
import { Button, Checkbox, Col, ControlLabel, Form, FormControl, FormGroup, Row, Glyphicon } from "react-bootstrap";
import { NotificationManager } from 'react-notifications';
import { get, put, post, del } from './../lib/http';


export class UserChallengeManager extends Component {
  displayName = UserChallengeManager.name;

  constructor(props) {
    super(props);
  }

  componentWillMount() {
    const userId = selectn('match.params.userId', this.props);
    UserChallengeManager.update(userId);
  }

  componentWillReceiveProps(nextProps) {
    const userId = selectn('match.params.userId', nextProps);
    if (userId !== selectn('match.params.userId', this.props)) {
      UserChallengeManager.update(userId);
    }
  }

  static update(userId) {
    if (userId === '_') {
      AppActions.updateStates([
        {
          statePath: 'user', data: {
            title: '',
            firstname: '',
            lastname: '',
            email: '',
            phone: '',
            dob:'',
            image: ''
          }
        },
        { statePath: 'loading', data: false }
      ]);
      return;
    }

    get('api/userchallenge/' + userId, (data) =>
      AppActions.updateStates([
        { statePath: 'user', data: data },
        { statePath: 'loading', data: false }
      ])
    );
  }

  save() {
    const { user, history } = this.props;
    user.image = this.state.image;

    if (user.id) {
      put('api/userchallenge/' + user.id, user, () => {
        NotificationManager.success('User saved');
        UserChallengeManager.update(user.id);
      });
  
      return;
    }

    post('api/userchallenge', user, (content) => {
      NotificationManager.success('User saved');
      history.push(`/userchallengemanager/${content.id}`);
    });
  }
  onImageChange = (event) => {
    if (event.target.files && event.target.files[0]) {
      let reader = new FileReader();
      reader.onload = (e) => {
        this.setState({ image: e.target.result });
      };
      reader.readAsDataURL(event.target.files[0]);
    }
  };   


  render() {
    const { loading, user } = this.props;
    const userId = selectn('match.params.userId', this.props);

    return (
      <div>
        <h1>User Manager</h1>

        <pre>{JSON.stringify(user, null, 2)}</pre>
        {user && <Form horizontal>
         
          <FormGroup controlId="email">
            <Col componentClass={ControlLabel} sm={2}>
              Email
                        </Col>
            <Col sm={10}>
              <FormControl type="text" value={user.email}
                onChange={bindValueTo('user.email')} />
            </Col>
          </FormGroup>

          <FormGroup controlId="title">
            <Col componentClass={ControlLabel} sm={2} >
              Title
                        </Col>
            <Col sm={10}>
              <FormControl type="text" value={user.title}
                onChange={bindValueTo('user.title')} />
            </Col>
          </FormGroup>


          <FormGroup controlId="firstname">
            <Col componentClass={ControlLabel} sm={2} >
              FirstName
                        </Col>
            <Col sm={10}>
              <FormControl type="text" value={user.firstName}
                onChange={bindValueTo('user.firstName')} />
            </Col>
          </FormGroup>

          <FormGroup controlId="lastname">
            <Col componentClass={ControlLabel} sm={2} >
              LastName
                        </Col>
            <Col sm={10}>
              <FormControl type="text" value={user.lastName}
                onChange={bindValueTo('user.lastName')} />
            </Col>
          </FormGroup>


          <FormGroup controlId="dob">
            <Col componentClass={ControlLabel} sm={2} >
              Date of Birth
                        </Col>
            <Col sm={10}>
              <FormControl type="date" id="start" name="dob-start" timeFormat={false}
                    min="1918-01-01" max="2020-03-31"
                onChange={bindValueTo('user.dob')} />
            </Col>
          </FormGroup>


          <FormGroup controlId="phone">
            <Col componentClass={ControlLabel} sm={2}>
              Phone
                        </Col>
            <Col sm={10}>
              <FormControl type="text" value={user.phone}
                onChange={bindValueTo('user.phone')} />
            </Col>
          </FormGroup>

          <FormGroup controlId="image">
            <Col componentClass={ControlLabel} sm={2}>
              Image
                        </Col>  
            <Col sm={10}>
              <img src={user.image} alt="No image" width="300px" height="300px" />
              <FormControl type="file" 
                onChange={this.onImageChange} accept="image/*"
                label="Change File"
              />

            </Col>
          </FormGroup>

          <FormGroup>
            <Col smOffset={2} sm={10}>
              <Button onClick={() => this.save()}>Save</Button>
            </Col>
          </FormGroup>
        </Form>}


        {user && user.id && <Button bsStyle="danger" bsSize="xsmall" onClick={() => this.del()}>Delete</Button>}
      </div>
    );
  }


  del() {
    const { history, user } = this.props;

    if (window.confirm("Do you really want to delete this?")) {
      del('api/userchallenge/' + user.id, () => {
        NotificationManager.success('User deleted');
        history.push('/userchallengemanager');
      });
    }
  }
}

UserChallengeManager.defaultProps = {
  loading: false,
  user: null
};