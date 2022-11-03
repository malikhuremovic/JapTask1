import React, { useCallback, useEffect, useState } from 'react';
import { Button, Form, Row, Col } from 'react-bootstrap';
import lectureService from '../../Services/lectureService';

const ProgramForm = ({ formType, handleFormSubmission, handleProgramFormInput, programFormData }) => {
  const [lectures, setLectures] = useState([]);
  const [selectedLectures, setSelectedLectures] = useState([]);
  const [selectedEvents, setSelectedEvents] = useState([]);
  const [currentSelectedLecture, setCurrentSelectedLecture] = useState(null);
  const [currentSelectedEvent, setCurrentSelectedEvent] = useState(null);
  const [showLectureSelectForm, setShowLectureSelectForm] = useState(false);
  const [showEventSelectForm, setShowEventSelectForm] = useState(false);

  const fetchAllLectures = useCallback(() => {
    lectureService
      .fetchAll()
      .then(response => setLectures(response.data.data))
      .catch(err => console.log(err));
  }, []);

  useEffect(() => {
    fetchAllLectures();
  }, [fetchAllLectures]);

  const handleShowSelectLectureForm = () => {
    setShowLectureSelectForm(prevState => !prevState);
  };

  const handleCurrentLecture = ev => {
    const value = JSON.parse(ev.target.value);
    if (value.id) setCurrentSelectedLecture(value);
  };

  const addCurrentLecture = () => {
    if (currentSelectedLecture) {
      setSelectedLectures(prevState => {
        const UPDATED_STATE = [...prevState, currentSelectedLecture];
        return UPDATED_STATE;
      });
      setLectures(prevState => {
        if (prevState.length >= 1) {
          const UPDATED_STATE = prevState.filter(el => el.id !== currentSelectedLecture.id);
          return UPDATED_STATE;
        }
        return prevState;
      });
      setCurrentSelectedLecture(null);
      setShowLectureSelectForm(false);
    }
  };

  const removeSelectedLecture = ev => {
    const value = +ev.target.previousSibling.value;
    setLectures(prevState => {
      const UPDATED_STATE = [...prevState];
      let arr = selectedLectures.filter(el => el.id === value);
      UPDATED_STATE.push(arr[0]);
      return UPDATED_STATE;
    });
    setSelectedLectures(prevState => {
      const UPDATED_STATE = prevState.filter(el => el.id !== value);
      return UPDATED_STATE;
    });
  };
  //
  const handleShowSelectEventForm = () => {
    setShowEventSelectForm(prevState => !prevState);
  };

  const handleCurrentEvent = ev => {
    const value = JSON.parse(ev.target.value);
    if (value.id) setCurrentSelectedEvent(value);
  };

  const addCurrentEvent = () => {
    if (currentSelectedEvent) {
      setSelectedEvents(prevState => {
        const UPDATED_STATE = [...prevState, currentSelectedEvent];
        return UPDATED_STATE;
      });
      setLectures(prevState => {
        if (prevState.length >= 1) {
          const UPDATED_STATE = prevState.filter(el => el.id !== currentSelectedEvent.id);
          return UPDATED_STATE;
        }
        return prevState;
      });
      setCurrentSelectedEvent(null);
      setShowEventSelectForm(false);
    }
  };

  const removeSelectedEvent = ev => {
    const value = +ev.target.previousSibling.value;
    setLectures(prevState => {
      const UPDATED_STATE = [...prevState];
      let arr = selectedEvents.filter(el => el.id === value);
      UPDATED_STATE.push(arr[0]);
      return UPDATED_STATE;
    });
    setSelectedEvents(prevState => {
      const UPDATED_STATE = prevState.filter(el => el.id !== value);
      return UPDATED_STATE;
    });
  };
  //

  const handleSubmit = ev => {
    ev.preventDefault();
    if (!formType === 'edit') {
      if (selectedLectures.length < 1) {
        alert('Please select at least one lecture');
        return;
      } else if (selectedEvents.length < 1) {
        alert('Please select at least one event');
      }
    }
    const items = [...selectedEvents, ...selectedLectures];
    handleFormSubmission(ev, items);
  };

  return (
    <React.Fragment>
      <Form onSubmit={handleSubmit}>
        <Form.Group as={Row} className="mb-3" controlId="formHorizontalFirstName">
          <Form.Label column sm={2}>
            Name
          </Form.Label>
          <Col sm={10}>
            <Form.Control
              name="name"
              type="text"
              placeholder="Enter name"
              onChange={handleProgramFormInput}
              value={programFormData.name}
              required
            />
          </Col>
        </Form.Group>
        <Form.Group as={Row} className="mb-3" controlId="formHorizontalLastName">
          <Form.Label column sm={2}>
            Content
          </Form.Label>
          <Col sm={10}>
            <Form.Control
              as="textarea"
              rows={3}
              name="content"
              placeholder="Enter the program topics separated by a comma"
              onChange={handleProgramFormInput}
              value={programFormData.content}
              required
            />
          </Col>
        </Form.Group>
        {formType !== 'edit' && (
          <div>
            {selectedLectures.length >= 1 && (
              <React.Fragment>
                <h6>Selected lectures</h6>
                <Form.Group as={Row} className="mb-3" controlId="formHorizontalStudentStatus">
                  <Col sm={10}>
                    {selectedLectures.map(lecture => {
                      return (
                        <React.Fragment>
                          <Form.Control
                            name="program"
                            type="text"
                            disabled
                            value={`${lecture.name} ${lecture.isEvent ? '| EVENT ' : ''}(${
                              lecture.expectedHours
                            } hours)`}
                          />
                          <input type="hidden" value={lecture.id} />
                          <Button style={{ margin: 5, marginLeft: 0 }} variant="danger" onClick={removeSelectedLecture}>
                            Remove
                          </Button>
                        </React.Fragment>
                      );
                    })}
                  </Col>
                </Form.Group>
              </React.Fragment>
            )}
            {showLectureSelectForm && (
              <React.Fragment>
                <Form.Group as={Row} className="mb-3" controlId="formHorizontalStudentStatus">
                  <Form.Label column sm={2}>
                    Available Lectures
                  </Form.Label>
                  <Col sm={10}>
                    <Form.Select
                      name="program"
                      className="form-select"
                      defaultValue=""
                      aria-label="Default select example"
                      required
                      onChange={handleCurrentLecture}
                    >
                      <option value="">Select a lecture</option>
                      {lectures.map(lecture => {
                        if (!lecture.isEvent)
                          return (
                            <option key={lecture.id} value={JSON.stringify(lecture)}>
                              {`${lecture.name} ${lecture.isEvent ? '| EVENT ' : ''}(${lecture.expectedHours} hours)`}
                            </option>
                          );
                      })}
                    </Form.Select>
                    <Button style={{ marginTop: 5 }} variant="success" onClick={addCurrentLecture}>
                      Add lecture
                    </Button>
                  </Col>
                </Form.Group>
              </React.Fragment>
            )}
            <Form.Group as={Row} className="mb-3" controlId="formHorizontalLastName">
              <Col sm={10}>
                <Button
                  style={{ width: '18%' }}
                  variant={showLectureSelectForm === false ? 'primary' : 'danger'}
                  onClick={handleShowSelectLectureForm}
                >
                  {showLectureSelectForm === false ? 'Add lectures' : 'Hide lectures'}
                </Button>
              </Col>
            </Form.Group>
            {selectedEvents.length >= 1 && (
              <React.Fragment>
                <h6>Selected events</h6>
                <Form.Group as={Row} className="mb-3" controlId="formHorizontalStudentStatus">
                  <Col sm={10}>
                    {selectedEvents.map(lecture => {
                      return (
                        <React.Fragment>
                          <Form.Control
                            name="program"
                            type="text"
                            disabled
                            value={`${lecture.name} ${lecture.isEvent ? '| EVENT ' : ''}(${
                              lecture.expectedHours
                            } hours)`}
                          />
                          <input type="hidden" value={lecture.id} />
                          <Button style={{ margin: 5, marginLeft: 0 }} variant="danger" onClick={removeSelectedEvent}>
                            Remove
                          </Button>
                        </React.Fragment>
                      );
                    })}
                  </Col>
                </Form.Group>
              </React.Fragment>
            )}
            {showEventSelectForm && (
              <React.Fragment>
                <Form.Group as={Row} className="mb-3" controlId="formHorizontalStudentStatus">
                  <Form.Label column sm={2}>
                    Available Events
                  </Form.Label>
                  <Col sm={10}>
                    <Form.Select
                      name="program"
                      className="form-select"
                      defaultValue=""
                      aria-label="Default select example"
                      required
                      onChange={handleCurrentEvent}
                    >
                      <option value="">Select an event</option>
                      {lectures.map(lecture => {
                        if (lecture.isEvent)
                          return (
                            <option key={lecture.id} value={JSON.stringify(lecture)}>
                              {`${lecture.name} ${lecture.isEvent ? '| EVENT ' : ''}(${lecture.expectedHours} hours)`}
                            </option>
                          );
                      })}
                    </Form.Select>
                    <Button style={{ marginTop: 5 }} variant="success" onClick={addCurrentEvent}>
                      Add event
                    </Button>
                  </Col>
                </Form.Group>
              </React.Fragment>
            )}
            <Form.Group as={Row} className="mb-3" controlId="formHorizontalLastName">
              <Col sm={10}>
                <Button
                  style={{ width: '18%' }}
                  variant={showEventSelectForm === false ? 'outline-success' : 'outline-danger'}
                  onClick={handleShowSelectEventForm}
                >
                  {showEventSelectForm === false ? 'Add events' : 'Hide events'}
                </Button>
              </Col>
            </Form.Group>
          </div>
        )}
        <Form.Group as={Row} className="mb-3">
          <Col sm={{ span: 10, offset: 2 }}>
            {formType === 'add' && (
              <Button style={{ width: '15%', position: 'absolute', left: 0 }} type="submit">
                Add new program
              </Button>
            )}
            {formType === 'edit' && (
              <Button style={{ width: 70, position: 'absolute', left: 0 }} type="submit" variant="success">
                Edit
              </Button>
            )}
          </Col>
        </Form.Group>
      </Form>
    </React.Fragment>
  );
};

export default ProgramForm;
