import { Alert, Box, Button, Container, Dialog, DialogActions, DialogContent, DialogTitle, Paper, Snackbar, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from "@mui/material"
import axios from "axios"
import { useEffect, useState } from "react"
import { useForm } from "react-hook-form"

interface Restaurante {
    id: number,
    nome: string,
}
const Restaurantes = () =>{
    const BASEURL = "https://localhost:7205/api/"
    const [restaurantes,setRestaurantes] = useState<Restaurante[]>([])
    const [openModal,setOpenModal] = useState(false)
    const [openComfirm,setOpenConfirm] = useState(false)
    const [restauranteSelecionado, setRestauranteSelecionado]= useState<Restaurante | null>(null)
    const [openSnackbar, setOpenSnackbar] = useState(false)
    const [mensagem, setMensagem] = useState("")

    const {register,
        handleSubmit,
        reset,
        formState:{errors}
    } = useForm<Restaurante>()
    const fetchRestaurantes = async () => {
        try{
            const response = await axios.get(`${BASEURL}Restaurante`)
            setRestaurantes(response.data)
        }catch(error){
            console.log(error)
        }
    }
    useEffect (() => {
        fetchRestaurantes()
    }, [])

    const handleOpenModal = (restaurante?: Restaurante) =>{
        setRestauranteSelecionado(restaurante || null)
        reset(restaurante || {nome: ""})
        setOpenModal(true)
    }
    const handleCloseModal = () => {
        setOpenModal(false)
        setRestauranteSelecionado(null)
    }
    const onSubmit = async(data:Restaurante) => {
        try{
            if(restauranteSelecionado){
                await axios.put(`${BASEURL}Restaurante/${restauranteSelecionado.id}`,data)
            }else{
                await axios.post(`${BASEURL}Restaurante`, data)
            }
            setMensagem("Restaurante cadastrado com sucesso!")
            setOpenSnackbar(true);
            fetchRestaurantes()
            handleCloseModal()
        }catch(error : any){
            if(error.response && error.response.data){
                setMensagem(error.response.data.mensagem)
            }else{
                setMensagem("Erro ao cadastrar restaurante")
            }
            setOpenSnackbar(true)
        }
    }
    const handleOpenConfirm = (restaurante: Restaurante) => {
        setRestauranteSelecionado(restaurante)
        setOpenConfirm(true)
    }
    const handleCloseConfirm = () => {
        setOpenConfirm(false)
        setRestauranteSelecionado(null)
    }

    const handleDelete = async () =>{
        if(restauranteSelecionado){
            try{
                await axios.delete(`${BASEURL}Restaurante/${restauranteSelecionado.id}`)
                fetchRestaurantes()
            }catch(error){
                console.log(error)
            }
            handleCloseConfirm();
        }
    }
    return (
        <Container maxWidth="lg">
            <Box sx= {{mt:4, mb: 2, display:"flex", justifyContent:"center", alignItems:"center"}}>
                <Typography variant="h5">Restaurantes Cadastrados </Typography>
                <Button style={{marginLeft:"10px"}} variant="contained" color="primary" onClick={() => handleOpenModal()}>
                    Adicionar Restaurante
                </Button>
            </Box>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Nome</TableCell>
                            <TableCell >Ações</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {restaurantes.map((restaurante) =>(
                            <TableRow key={restaurante.id}>
                                <TableCell>{restaurante.nome}</TableCell>
                                <TableCell >
                                    <Button color="primary" onClick={() => handleOpenModal(restaurante)}>
                                        Editar
                                    </Button>
                                    <Button color="error" onClick={() => handleOpenConfirm(restaurante)}>
                                        Excluir
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Dialog open={openModal} onClose={handleCloseModal}>
                <DialogTitle>{restauranteSelecionado ? "Editar Restaurante" : "Adicionar Restaurante"}</DialogTitle>
                <DialogContent>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <TextField
                            label="Nome"
                            fullWidth
                            margin="normal"
                            {...register("nome",{required: "Nome é obrigatório"})}
                            error={!!errors.nome}
                            helperText={errors.nome?.message}
                        />
                        <DialogActions>
                            <Button onClick={handleCloseModal} color="secondary">
                                Cancelar
                            </Button>
                            <Button type="submit" variant="contained" color="primary">
                                Salvar
                            </Button>
                        </DialogActions>
                    </form>
                </DialogContent>
            </Dialog>
            <Dialog open={openComfirm} onClose={handleCloseConfirm}>
                <DialogTitle>Confirmar Exclusão</DialogTitle>
                <DialogContent>
                    <Typography>
                        Tem certeza que deseja excluir "{restauranteSelecionado?.nome}"?
                    </Typography>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseConfirm} color="secondary">
                        Cancelar
                    </Button>
                    <Button onClick={handleDelete} variant="contained" color="primary">
                        Excluir
                    </Button>
                </DialogActions>
            </Dialog>
            <Snackbar 
                open={openSnackbar} 
                autoHideDuration={3000} 
                onClose={() => setOpenSnackbar(false)}
            >
                <Alert
                    severity={mensagem.includes("sucesso") ? "success" : "error"}
                    onClose={() => setOpenSnackbar(false)}
                >
                    {mensagem}
                </Alert>
            </Snackbar>
        </Container>
    )
}
export default Restaurantes