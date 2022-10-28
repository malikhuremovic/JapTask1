import React, { useCallback, useEffect, useState } from 'react';
import { Button, Form, Row, Col } from 'react-bootstrap';
import lectureService from '../../Services/lectureService';

const ProgramAddItemForm = ({ formType, availableItems, handleFormSubmission }) => {
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
      .then(response => {
        const filteredAvailableItems = [];
        const fetchedItems = response.data.data;
        fetchedItems.map(element => {
          let exists = false;
          availableItems.map(existingElement => {
            if (element.id === existingElement.id) {
              exists = true;
            }
          });

          if (!exists) {
            filteredAvailableItems.push(element);
          }
        });
        setLectures(filteredAvailableItems);
      })
      .catch(err => console.log(err));
  }, [availableItems]);

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
    const items = [...selectedEvents, ...selectedLectures].map(item => item.id);
    handleFormSubmission(items);
  };

  return (
    <React.Fragment>
      <Form onSubmit={handleSubmit}>
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
                          value={`${lecture.name} ${lecture.isEvent ? '| EVENT ' : ''}(${lecture.expectedHours} hours)`}
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
                style={{ width: '35%' }}
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
                          value={`${lecture.name} ${lecture.isEvent ? '| EVENT ' : ''}(${lecture.expectedHours} hours)`}
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
                style={{ width: '35%' }}
                variant={showEventSelectForm === false ? 'outline-success' : 'outline-danger'}
                onClick={handleShowSelectEventForm}
              >
                {showEventSelectForm === false ? 'Add events' : 'Hide events'}
              </Button>
            </Col>
          </Form.Group>
        </div>
        <Form.Group as={Row} className="mb-3">
          <Col sm={{ span: 10, offset: 2 }}>
            {formType === 'add' && (
              <Button style={{ width: '35%', position: 'absolute', left: 0 }} type="submit">
                Edit program
              </Button>
            )}
          </Col>
        </Form.Group>
      </Form>
    </React.Fragment>
  );
};

export default ProgramAddItemForm;
