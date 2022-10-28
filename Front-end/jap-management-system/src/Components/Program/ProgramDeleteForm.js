import { Form, Row, Col, Button } from 'react-bootstrap';

const ProgramDeleteForm = ({ handleDeleteProgram }) => {
  return (
    <Form onSubmit={handleDeleteProgram}>
      <Row className="align-items-center">
        <Col xs="auto">
          <Form.Label>
            Are you aware that by performing this action, the program, and all selections associated with it will be
            removed from the JAP platform?
          </Form.Label>
        </Col>
        <Col xs="auto">
          <Button type="submit" variant="danger" className="mb-2">
            I am aware, Delete program
          </Button>
        </Col>
      </Row>
    </Form>
  );
};

export default ProgramDeleteForm;
