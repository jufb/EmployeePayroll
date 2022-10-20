import { useEffect, useRef, useState } from 'react';
import { Alert, Container, FormControl, Table, Button, Form } from 'react-bootstrap';

export function UploadFile() {
    const [file, setFile] = useState('');
    const [payrollReport, setPayrollReport] = useState();
    const ref = useRef(null);

    const [isLoading, setLoading] = useState(false);

    const [message, setMessage] = useState('');
    const [variant, setVariant] = useState();

    const fileChange = e => {
        setFile(e.target.files[0]);
    };
    const handleClick = () => {
        setLoading(true);

        if (file.length === 0) {
            setVariant('danger');
            setMessage('Please include your CSV file to upload it.');
            setLoading(false);
            return null;
        }

        postData();

        setLoading(false);
    };

    function setAlert(variant, message) {
        setVariant(variant);
        setMessage(message);
    }

    async function fetchData() {
        const response = await fetch('https://localhost:7150/api/timereport', {
            method: 'GET',
            mode: 'cors'
        });

        return await response.json();
    }

    async function postData() {
        const formData = new FormData();
        formData.append('file', file);

        const response = await fetch('https://localhost:7150/api/timereport', {
                method: 'POST',
                body: formData,
                mode: 'cors'
            })
            .catch((err) => console.error('Error' + err));

        if(response.ok) {
            setAlert('success', 'Data was successfully imported.');

            if(response.status === 204) setAlert('info', '204 No Content - No data was included because the file had no rows.');

            try {
                const data = await response.clone().json();
                return setPayrollReport(data.payrollReport);
            } catch (e) {
                console.error("Couldn't parse error response for specific message. " + e);
            }
        }
        else {
            setAlert('danger', `${response.status} ${response.statusText} - Time Report Id of the file ${file.name} already exists. Please try again with a new file.`);
            console.log(file)
        }
    }

    useEffect(() => {
        setLoading(true);

        fetchData()
            .then((data) => {
                setPayrollReport(data.payrollReport);
            })
            .catch((error) => {
                setAlert('danger', `Error: ${error}`);
            });

        setLoading(false);
    }, []);


    return (
        <main>
            {message.length > 0 && 
                <Alert key={variant} variant={variant}><center>{message}</center></Alert>
            }
            
            <Container style={{paddingTop:40, paddingBottom:40}}>
                <label><strong>Include your CSV file:</strong></label>
                <Form className="d-flex">
                    <FormControl
                        type="file"
                        ref={ref}
                        disabled={isLoading}
                        onChange={fileChange}
                        accept=".csv" />
                    <Button variant="primary" 
                        disabled={isLoading}
                        onClick={!isLoading ? handleClick : null}>
                            {isLoading ? 'Loading...' : 'Upload'}
                    </Button>
                </Form>
            </Container>

            <Container>
                {isLoading && <center>Loading...</center>}
                {(payrollReport != null && payrollReport.employeeReports.length > 0) ?
                    <Table striped bordered hover>
                        <thead>
                            <tr>
                                <th>Employee ID</th>
                                <th>Pay Period Start</th>
                                <th>Pay Period End</th>
                                <th>Amount Paid</th>
                            </tr>
                        </thead>
                        <tbody>
                            {payrollReport.employeeReports.map((report, id) =>
                                <tr key={id}>
                                    <td>{report.employeeId}</td>
                                    <td>{report.payPeriod.startDate}</td>
                                    <td>{report.payPeriod.endDate}</td>
                                    <td>{report.amountPaid}</td>
                                </tr>
                            )}
                        </tbody>
                    </Table>
                : <center>No results.</center>}
            </Container>
        </main>
    );
}